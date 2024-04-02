import {
  getProfileDetails,
  getProfileComments,
  ProfileCommentsSectionResponseType,
  ProfileResponseType,
  ProfileCommentResponseType,
  addProfileComment,
  ProfileCommentRequestType,
  editProfileComment,
  deleteProfileComment,
  BaseProfileType,
} from 'commons';
import React, { Dispatch, SetStateAction } from 'react';
import { FlatList, Pressable, Text, View } from 'react-native';
import {
  dark,
  dark2,
  DeleteButton,
  light,
  light3,
  LoadingNextPageView,
  LoadingView,
  MainContainer,
  primary,
  ScoreView,
  secondary,
  SecondaryButton,
  StarRating,
} from '../../Components';
import { getAccessToken } from '../../SecureStorage';
import { TextInput } from 'react-native-gesture-handler';
import { Appbar, Avatar } from 'react-native-paper';
import { PaginationMetadata } from 'commons/lib/http';
import Snackbar from 'react-native-snackbar';

const validationSnackBar = (text: string) => {
  Snackbar.show({
    text,
    duration: Snackbar.LENGTH_LONG,
    action: {
      text: 'Zamknij',
      textColor: primary,
    },
  });
};

const CommentItem = (props: any) => {
  const item: ProfileCommentResponseType = props.item;
  const navigate: () => Promise<void> = props.navigate;

  return (
    <View
      style={{
        marginTop: 20,
        padding: 10,
        borderRadius: 10,
        borderWidth: 1,
        borderColor: dark2,
        backgroundColor: light,
      }}>
      <View
        style={{
          flexDirection: 'row',
          justifyContent: 'space-between',
        }}>
        <Pressable
          style={{ flexDirection: 'row', justifyContent: 'space-between' }}
          onPress={navigate}>
          <View style={{ marginRight: 10 }}>
            {item.author.pictureUrl ? (
              <Avatar.Image
                source={{
                  uri: item.author.pictureUrl,
                }}
                style={{
                  backgroundColor: light3,
                }}
                size={30}
              />
            ) : (
              <Avatar.Icon
                icon={'account'}
                size={30}
                style={{
                  alignSelf: 'center',
                  backgroundColor: light3,
                }}
              />
            )}
          </View>
          <Text style={{ fontSize: 18, fontWeight: '500', color: dark }}>
            {item.author.username}
          </Text>
        </Pressable>
        <ScoreView score={item.profileRating} />
      </View>
      <Text>{item.content}</Text>
    </View>
  );
};

const MyComment = (props: {
  item?: ProfileCommentResponseType;
  userId: string;
  update: boolean;
  updateHandler: Dispatch<SetStateAction<boolean>>;
}) => {
  const item = props.item;
  const userId = props.userId;
  const [profileRating, setProfileRating] = React.useState<number>(0);
  const [commentContent, setCommentContent] = React.useState<
    string | undefined
  >(props.item?.content);

  React.useEffect(() => {
    setProfileRating(item?.profileRating ? item.profileRating : 0);
    setCommentContent(item?.content);
  }, [item]);

  return (
    <View
      style={{
        marginTop: 20,
        padding: 10,
        borderRadius: 10,
        borderWidth: 1,
        borderColor: dark2,
        backgroundColor: light,
      }}>
      <View
        style={{
          flexDirection: 'row',
          justifyContent: 'space-between',
        }}>
        <SecondaryButton
          label={item ? 'Edytuj komentarz' : 'Zostaw komentarz'}
          onPress={async () => {
            const myComment: ProfileCommentRequestType = {
              content: commentContent,
              profileRating: profileRating,
            };
            if (item) {
              await leaveComment(userId, myComment, true);
            } else {
              await leaveComment(userId, myComment, false);
            }
            props.updateHandler(!props.update);
          }}
        />
        <StarRating
          starRating={profileRating}
          ratingHandler={setProfileRating}
        />
      </View>
      <TextInput
        onChangeText={setCommentContent}
        value={commentContent}
        keyboardType={'default'}
        placeholder="Zostaw komentarz"
      />
      {item ? (
        <DeleteButton
          label="Usuń komentarz"
          onPress={async () => {
            await deleteMyComment(userId);
            props.updateHandler(!props.update);
          }}
        />
      ) : (
        <></>
      )}
    </View>
  );
};

async function leaveComment(
  userId: string,
  content: ProfileCommentRequestType,
  commentExists: boolean,
) {
  const accessToken = await getAccessToken();

  if (!content.content || !/\S+/.test(content.content)) {
    validationSnackBar('Komentarz nie może być pusty');
    return;
  }
  if (accessToken) {
    if (commentExists) {
      editProfileComment(userId, content, accessToken);
    } else {
      addProfileComment(userId, content, accessToken);
    }
  }
}

async function deleteMyComment(userId: string) {
  const accessToken = await getAccessToken();
  if (accessToken) {
    await deleteProfileComment(userId, accessToken);
  }
}

export const ProfilePage = (props: any) => {
  const userId = props.route.params?.userId;
  const [width, setWidth] = React.useState<number>(10);
  const [profile, setProfile] = React.useState<ProfileResponseType>();
  const [profileComments, setProfileComments] =
    React.useState<ProfileCommentsSectionResponseType>();
  const [update, setUpdate] = React.useState<boolean>(false);
  const [pageNumber, setPageNumber] = React.useState<number>(1);
  const [pagination, setPagination] = React.useState<PaginationMetadata>();
  const [loading, setLoading] = React.useState<boolean>(true);
  const [loadingNextPage, setLoadingNextPage] = React.useState<boolean>(false);

  React.useEffect(() => {
    const getData = async () => {
      const accessToken = await getAccessToken();
      if (accessToken) {
        setProfile(await getProfileDetails(userId, accessToken));
      }
    };

    getData();
  }, [update]);

  React.useEffect(() => {
    const getData = async () => {
      const accessToken = await getAccessToken();
      if (accessToken && profile) {
        const responseData = await getProfileComments(
          profile.userId,
          accessToken,
          pageNumber,
        );
        setProfileComments(responseData?.commentsSection);
        setPagination(responseData?.pagination);
        setLoading(false);
      }
    };

    getData();
  }, [profile]);

  const HeaderBar = () => {
    return (
      <Appbar.Header style={{ backgroundColor: secondary }}>
        <Appbar.BackAction
          color={light}
          onPress={() => props.navigation.pop()}
        />
        <Appbar.Content
          title={profile?.username}
          titleStyle={{
            textAlign: 'center',
            color: light,
            fontWeight: 'bold',
          }}
        />
        <Appbar.Action
          size={30}
          icon="chat"
          color={light}
          onPress={() => {
            if (profile) {
              const chatRecipent: BaseProfileType = {
                userId: profile.userId,
                username: profile.username,
                pictureUrl: profile.pictureUrl,
              };
              props.navigation.push('Home', {
                screen: 'Chat',
                params: {
                  chatRecipent,
                },
              });
            }
          }}
        />
      </Appbar.Header>
    );
  };

  if (loading) {
    return (
      <MainContainer>
        <HeaderBar />
        <LoadingView />
      </MainContainer>
    );
  }

  return (
    <MainContainer>
      <HeaderBar />
      <FlatList
        style={{ paddingHorizontal: 30, marginTop: 30 }}
        ListHeaderComponent={() => (
          <View>
            <View
              style={{
                flexDirection: 'row',
                justifyContent: 'space-between',
                marginTop: 10,
                marginBottom: 10,
              }}
              onLayout={event => setWidth(event.nativeEvent.layout.width)}>
              {profile?.pictureUrl ? (
                <Avatar.Image
                  source={{
                    uri: profile.pictureUrl,
                  }}
                  style={{
                    marginBottom: 20,
                    backgroundColor: light3,
                  }}
                  size={(width * 4) / 9}
                />
              ) : (
                <Avatar.Icon
                  icon={'account'}
                  size={(width * 4) / 9}
                  style={{
                    alignSelf: 'center',
                    marginTop: 10,
                    marginRight: 30,
                    backgroundColor: light3,
                  }}
                />
              )}
              <View
                style={{
                  flex: 1,
                  width: (width * 5) / 9,
                  paddingLeft: 20,
                }}>
                <View
                  style={{
                    flex: 2,
                    flexDirection: 'row',
                    marginBottom: 10,
                  }}>
                  <Text style={{ fontSize: 18, flex: 3 }}>{`${
                    profile?.name ? `${profile.name} ` : ''
                  }${profile?.surname ? profile.surname : ''}`}</Text>
                  <ScoreView score={profile?.averageProfileRating} />
                </View>
              </View>
            </View>
            <Text style={{ fontSize: 18 }}>{profile?.city}</Text>
            <Text>{profile?.description}</Text>
            <MyComment
              item={profileComments?.myComment}
              userId={userId}
              update={update}
              updateHandler={setUpdate}
            />
          </View>
        )}
        contentContainerStyle={{ paddingBottom: 20 }}
        data={profileComments?.comments}
        keyExtractor={item => item.author.id.toString()}
        renderItem={({ item }) => (
          <CommentItem
            item={item}
            navigate={async () => {
              props.navigation.push('Home', {
                screen: 'Profile',
                params: { userId: item.author.id },
              });
            }}
          />
        )}
        onEndReached={() => {
          const getData = async () => {
            setLoadingNextPage(true);
            if (pagination && pageNumber < pagination?.TotalPageCount) {
              const accessToken = await getAccessToken();
              if (accessToken && profile) {
                const responseData = await getProfileComments(
                  profile.userId,
                  accessToken,
                  pageNumber + 1,
                );
                if (profileComments && responseData) {
                  setProfileComments({
                    myComment: responseData?.commentsSection.myComment,
                    comments: [
                      ...profileComments?.comments,
                      ...responseData?.commentsSection.comments,
                    ],
                  });
                  setPagination(responseData?.pagination);
                  setPageNumber(pageNumber + 1);
                }
              }
            }
            setLoadingNextPage(false);
          };

          getData();
        }}
        ListFooterComponent={() => {
          return (
            <View style={{ marginTop: 30, marginBottom: 10 }}>
              {loadingNextPage ? <LoadingNextPageView /> : <></>}
            </View>
          );
        }}
      />
    </MainContainer>
  );
};
