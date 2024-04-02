export interface Post {
  _id: string;
  userId: string;
  creationDate: Date;
  incidentDate: Date;
  location: string;
  description: string;
  title: string;
  category: string;
  state: PostState;
  votesScore: number;
  votesNumber: number;
  modifiedDate: Date;
  type: PostType;
}

export interface PostVote {
  _id: string;
  userId: string;
  voteDate: Date;
  typeOfVote: boolean;
}

const enum PostState {
  Active,
  Closed,
}

const enum PostType {
  Lost,
  Found,
}

export const GetPosts = () => {
  const posts: Post[] = [
    {
      _id: '1',
      userId: '1',
      creationDate: new Date(),
      incidentDate: new Date(),
      location: 'Warszawa 02-500, Aleje Jerozolimskie 53/12',
      description: `Lorem ipsum dolor sit amet, consectetur adipiscing elit, sed do eiusmod tempor incididunt ut labore et dolore magna aliqua. 
Ut enim ad minim veniam, quis nostrud exercitation ullamco laboris nisi ut aliquip ex ea commodo consequat. 
Duis aute irure dolor in reprehenderit in voluptate velit esse cillum dolore eu fugiat nulla pariatur. 
Excepteur sint occaecat cupidatat non proident, sunt in culpa qui off
icia deserunt mollit anim id est laborum.`,
      title: 'Pierwszy Post z Bardzo długim tytułem',
      category: 'Ubrania',
      state: PostState.Active,
      votesScore: 13,
      votesNumber: 20,
      modifiedDate: new Date(),
      type: PostType.Found,
    },
    {
      _id: '2',
      userId: '2',
      creationDate: new Date(),
      incidentDate: new Date(),
      location: 'Aleje Jerozolimskie 53/12',
      description: `Lorem ipsum dolor sit amet, 
consectetur adipiscing elit, sed do eiusmod tempor incididunt ut labore et dolore magna aliqua. 
Ut enim ad minim veniam, quis nostrud exercitation ullamco laboris nisi ut aliquip ex ea commodo consequat. 
Duis aute irure dolor in reprehenderit in voluptate velit esse cillum dolore eu fugiat nulla pariatur. 
Excepteur sint occaecat cupidatat non proident, sunt in culpa qui off
icia deserunt mollit anim id est laborum.`,
      title: 'Drugi Post',
      category: 'ubrania',
      state: PostState.Active,
      votesScore: -6,
      votesNumber: 20,
      modifiedDate: new Date(),
      type: PostType.Found,
    },
    {
      _id: '3',
      userId: '3',
      creationDate: new Date(),
      incidentDate: new Date(),
      location: 'Aleje Jerozolimskie 53/12',
      description: `Lorem ipsum dolor sit amet, 
consectetur adipiscing elit, sed do eiusmod tempor incididunt ut labore et dolore magna aliqua. 
Ut enim ad minim veniam, quis nostrud exercitation ullamco laboris nisi ut aliquip ex ea commodo consequat. 
Duis aute irure dolor in reprehenderit in voluptate velit esse cillum dolore eu fugiat nulla pariatur. 
Excepteur sint occaecat cupidatat non proident, sunt in culpa qui off
icia deserunt mollit anim id est laborum.`,
      title: 'Trzeci Post',
      category: 'ubrania',
      state: PostState.Active,
      votesScore: 13,
      votesNumber: 20,
      modifiedDate: new Date(),
      type: PostType.Found,
    },
    {
      _id: '4',
      userId: '4',
      creationDate: new Date(),
      incidentDate: new Date(),
      location: 'Aleje Jerozolimskie 53/12',
      description: `Lorem ipsum dolor sit amet, 
consectetur adipiscing elit, sed do eiusmod tempor incididunt ut labore et dolore magna aliqua. 
Ut enim ad minim veniam, quis nostrud exercitation ullamco laboris nisi ut aliquip ex ea commodo consequat. 
Duis aute irure dolor in reprehenderit in voluptate velit esse cillum dolore eu fugiat nulla pariatur. 
Excepteur sint occaecat cupidatat non proident, sunt in culpa qui off
icia deserunt mollit anim id est laborum.`,
      title: 'Czwarty Post',
      category: 'ubrania',
      state: PostState.Active,
      votesScore: 13,
      votesNumber: 20,
      modifiedDate: new Date(),
      type: PostType.Found,
    },
    {
      _id: '5',
      userId: '5',
      creationDate: new Date(),
      incidentDate: new Date(),
      location: 'Aleje Jerozolimskie 53/12',
      description: `Lorem ipsum dolor sit amet, 
consectetur adipiscing elit, sed do eiusmod tempor incididunt ut labore et dolore magna aliqua. 
Ut enim ad minim veniam, quis nostrud exercitation ullamco laboris nisi ut aliquip ex ea commodo consequat. 
Duis aute irure dolor in reprehenderit in voluptate velit esse cillum dolore eu fugiat nulla pariatur. 
Excepteur sint occaecat cupidatat non proident, sunt in culpa qui off
icia deserunt mollit anim id est laborum.`,
      title: 'Piąty Post',
      category: 'ubrania',
      state: PostState.Active,
      votesScore: 13,
      votesNumber: 20,
      modifiedDate: new Date(),
      type: PostType.Found,
    },
  ];

  return posts;
};
