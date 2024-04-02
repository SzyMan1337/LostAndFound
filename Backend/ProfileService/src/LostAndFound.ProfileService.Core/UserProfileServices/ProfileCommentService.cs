using AutoMapper;
using LostAndFound.ProfileService.Core.DateTimeProviders;
using LostAndFound.ProfileService.Core.UserProfileServices.Interfaces;
using LostAndFound.ProfileService.CoreLibrary.Exceptions;
using LostAndFound.ProfileService.CoreLibrary.Internal;
using LostAndFound.ProfileService.CoreLibrary.Requests;
using LostAndFound.ProfileService.CoreLibrary.ResourceParameters;
using LostAndFound.ProfileService.CoreLibrary.Responses;
using LostAndFound.ProfileService.DataAccess.Entities;
using LostAndFound.ProfileService.DataAccess.Repositories.Interfaces;

namespace LostAndFound.ProfileService.Core.UserProfileServices
{
    public class ProfileCommentService : IProfileCommentService
    {
        private readonly IProfilesRepository _profilesRepository;
        private readonly IDateTimeProvider _dateTimeProvider;
        private readonly IMapper _mapper;

        public ProfileCommentService(IProfilesRepository profilesRepository, IDateTimeProvider dateTimeProvider, IMapper mapper)
        {
            _profilesRepository = profilesRepository ?? throw new ArgumentNullException(nameof(profilesRepository));
            _dateTimeProvider = dateTimeProvider ?? throw new ArgumentNullException(nameof(dateTimeProvider));
            _mapper = mapper ?? throw new ArgumentNullException(nameof(mapper));
        }

        public async Task<CommentDataResponseDto> CreateProfileComment(string rawUserId, string username,
            CreateProfileCommentRequestDto commentRequestDto, Guid profileOwnerId)
        {
            var userId = ParseUserId(rawUserId);
            if (userId == profileOwnerId)
            {
                throw new BadRequestException("The user cannot comment on his own profile.");
            }

            var profileEntity = await GetUserProfile(profileOwnerId);

            var commentEntity = _mapper.Map<Comment>(commentRequestDto);
            if (commentEntity == null)
            {
                throw new BadRequestException("The comment data is incorrect.");
            }
            commentEntity.CreationTime = commentEntity.LastModificationDate = _dateTimeProvider.UtcNow;
            commentEntity.AuthorId = userId;
            commentEntity.AuthorUsername = username;

            bool userCommentExists = profileEntity.Comments.Any(c => c.AuthorId == userId);
            if (userCommentExists)
            {
                throw new ConflictException("The comment already exists.");
            }

            await _profilesRepository.InsertNewProfileComment(profileOwnerId, commentEntity);

            return await GetUserProfileCommentData(profileOwnerId, userId);
        }

        public async Task DeleteProfileComment(string rawUserId, Guid profileOwnerId)
        {
            var userId = ParseUserId(rawUserId);
            var profileEntity = await GetUserProfile(profileOwnerId);
            var commentEntity = GetUserCommentFromProfile(userId, profileEntity);

            await _profilesRepository.DeleteProfileComment(profileOwnerId, commentEntity);
        }

        public async Task<(ProfileCommentsSectionResponseDto, PaginationMetadata)> GetProfileCommentsSection(string rawUserId,
            Guid profileOwnerId, ProfileCommentsResourceParameters resourceParameters)
        {
            var userId = ParseUserId(rawUserId);
            var commentsList = (await GetUserProfile(profileOwnerId)).Comments.ToList();
            var commentsSectionDto = new ProfileCommentsSectionResponseDto();

            var userComment = commentsList.SingleOrDefault(com => com.AuthorId == userId);
            if (userComment != null)
            {
                commentsSectionDto.MyComment = _mapper.Map<CommentDataResponseDto>(userComment);
                commentsSectionDto.MyComment.Author.PictureUrl = (await GetUserProfile(userId))?.PictureUrl;
            }

            var commentPage = commentsList.Where(com => com.AuthorId != userId)
                .OrderByDescending(com => com.CreationTime)
                .Skip(resourceParameters.PageSize * (resourceParameters.PageNumber - 1))
                .Take(resourceParameters.PageSize)
                .ToList();

            if (commentPage != null && commentPage.Any())
            {
                commentsSectionDto.Comments = _mapper.Map<IEnumerable<CommentDataResponseDto>>(commentPage);
                await SetCommentsAuthorPictureUrls(commentsSectionDto.Comments);
            }

            int totalItemCount = commentsList.Count - (userComment == null ? 0 : 1);
            var paginationMetadata = new PaginationMetadata(totalItemCount, resourceParameters.PageSize, resourceParameters.PageNumber);

            return (commentsSectionDto, paginationMetadata);
        }

        public async Task<CommentDataResponseDto> UpdateProfileComment(string rawUserId,
            UpdateProfileCommentRequestDto commentRequestDto, Guid profileOwnerId)
        {
            var userId = ParseUserId(rawUserId);
            var profileEntity = await GetUserProfile(profileOwnerId);
            var commentEntity = GetUserCommentFromProfile(userId, profileEntity);

            _mapper.Map(commentRequestDto, commentEntity);
            commentEntity.LastModificationDate = _dateTimeProvider.UtcNow;
            await _profilesRepository.UpdateProfileComment(profileOwnerId, commentEntity);

            return await GetUserProfileCommentData(profileOwnerId, userId);
        }

        private async Task<CommentDataResponseDto> GetUserProfileCommentData(Guid profileOwnerId, Guid userId)
        {
            var comment = (await _profilesRepository.GetSingleAsync(x => x.UserId == profileOwnerId))
                ?.Comments?.SingleOrDefault(c => c.AuthorId == userId);

            return _mapper.Map<CommentDataResponseDto>(comment);
        }

        private async Task<DataAccess.Entities.Profile> GetUserProfile(Guid profileOwnerId)
        {
            var profileEntity = await _profilesRepository.GetSingleAsync(x => x.UserId == profileOwnerId);
            if (profileEntity == null)
            {
                throw new NotFoundException("User profile not found.");
            }

            return profileEntity;
        }

        private async Task SetCommentsAuthorPictureUrls(IEnumerable<CommentDataResponseDto> commentsList)
        {
            var authorsIds = commentsList.Select(x => x.Author.Id);
            var authorProfiles = (await _profilesRepository
                .FilterByAsync(x => authorsIds.Contains(x.UserId))).ToList();

            foreach (var comment in commentsList)
            {
                var authorPictureUrl = authorProfiles.FirstOrDefault(u => u.UserId == comment.Author.Id)?.PictureUrl;
                comment.Author.PictureUrl = authorPictureUrl;
            }
        }

        private static Guid ParseUserId(string rawUserId)
        {
            if (!Guid.TryParse(rawUserId, out Guid userId))
            {
                throw new UnauthorizedException();
            }

            return userId;
        }

        private static Comment GetUserCommentFromProfile(Guid userId, DataAccess.Entities.Profile profileEntity)
        {
            var commentEntity = profileEntity.Comments.SingleOrDefault(x => x.AuthorId == userId);
            if (commentEntity == null)
            {
                throw new NotFoundException("Comment does not exist.");
            }

            return commentEntity;
        }
    }
}
