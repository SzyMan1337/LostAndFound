export interface Profile {
  _id: string;
  userId: string;
  email: string;
  username: string;
  name: string;
  surname: string;
  city: string;
  profileDescription: string;
  averageScore: number;
}

export interface Vote {
  _id: string;
  userId: string;
  username: string;
  score: number;
  comment: string;
  date: Date;
  editDate: Date;
}

export const GetProfiles = () => {
  const posts: Profile[] = [
    {
      _id: '1',
      userId: '1',
      email: 'test1@gmail.com',
      username: 'Nazwa użytkownika',
      name: 'Imię',
      surname: 'Nazwisko',
      city: 'Tomaszów Mazowiecki',
      profileDescription: `Lorem ipsum dolor sit amet, 
consectetur adipiscing elit, sed do eiusmod tempor incididunt ut labore et dolore magna aliqua. 
Ut enim ad minim veniam, quis nostrud exercitation ullamco laboris nisi ut aliquip ex ea commodo consequat. 
Duis aute irure dolor in reprehenderit in voluptate velit esse cillum dolore eu fugiat nulla pariatur. 
Excepteur sint occaecat cupidatat non proident, sunt in culpa qui off
icia deserunt mollit anim id est laborum.`,
      averageScore: 4.5,
    },
    {
      _id: '2',
      userId: '2',
      email: 'test2@gmail.com',
      username: 'Nazwa użytkownika',
      name: 'Imię',
      surname: 'Nazwisko',
      city: 'Warszawa',
      profileDescription: `Lorem ipsum dolor sit amet, 
consectetur adipiscing elit, sed do eiusmod tempor incididunt ut labore et dolore magna aliqua. 
Ut enim ad minim veniam, quis nostrud exercitation ullamco laboris nisi ut aliquip ex ea commodo consequat. 
Duis aute irure dolor in reprehenderit in voluptate velit esse cillum dolore eu fugiat nulla pariatur. 
Excepteur sint occaecat cupidatat non proident, sunt in culpa qui off
icia deserunt mollit anim id est laborum.`,
      averageScore: 4.5,
    },
  ];

  return posts;
};

export const GetVotes = () => {
  const votes: Vote[] = [
    {
      _id: '1',
      userId: '1',
      username: 'Nazwa użytkownika',
      score: 5,
      comment: 'Bardzo fajnie',
      date: new Date(),
      editDate: new Date(),
    },
    {
      _id: '2',
      userId: '1',
      username: 'Nazwa użytkownika',
      score: 4,
      comment: `Lorem ipsum dolor sit amet, consectetur adipiscing elit, sed do eiusmod tempor incididunt ut labore et dolore magna aliqua.`,
      date: new Date(),
      editDate: new Date(),
    },
    {
      _id: '3',
      userId: '1',
      username: 'Nazwa użytkownika',
      score: 4,
      comment: `Lorem ipsum dolor sit amet, consectetur adipiscing elit, sed do eiusmod tempor incididunt ut labore et dolore magna aliqua.`,
      date: new Date(),
      editDate: new Date(),
    },
    {
      _id: '4',
      userId: '1',
      username: 'Nazwa użytkownika',
      score: 4,
      comment: `Lorem ipsum dolor sit amet, consectetur adipiscing elit, sed do eiusmod tempor incididunt ut labore et dolore magna aliqua.`,
      date: new Date(),
      editDate: new Date(),
    },
  ];

  return votes;
};
