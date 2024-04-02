import { NavigationContainer } from '@react-navigation/native';
import * as React from 'react';
import { Provider as PaperProvider } from 'react-native-paper';
import { HubConnection, HubConnectionBuilder } from '@microsoft/signalr';
import {
  AuthContext,
  ProfileContext,
  setUnreadChatsCount,
} from './src/Context';
import { AuthScreenStack, HomeScreenStack } from './src/Navigation';
import { getAccessToken } from './src/SecureStorage';
import { clearStorage } from './src/SecureStorage/Authorization';
import { getUnreadNotifications, MessageResponseType } from 'commons';

const App = () => {
  const [connection, setConnection] = React.useState<HubConnection>();
  const [updatePhotoUrlValue, setUpdatePhotoUrlValue] =
    React.useState<boolean>(false);
  const [updateChatsValue, setUpdateChats] = React.useState<boolean>(false);
  const [unreadChats, setUnreadChats] = React.useState<number>(0);
  const [chatMessage, setChatMessage] = React.useState<MessageResponseType>();

  const [state, dispatch] = React.useReducer(
    (prevState: any, action: { type: any }) => {
      switch (action.type) {
        case 'SIGN_IN':
          return {
            ...prevState,
            isSignedIn: true,
          };
        case 'SIGN_OUT':
          return {
            ...prevState,
            isSignedIn: false,
          };
      }
    },
    {
      isLoading: true,
      isSignedIn: false,
    },
  );

  const authContext = React.useMemo(
    () => ({
      signIn: async () => {
        const token = await getAccessToken();
        if (token) {
          dispatch({ type: 'SIGN_IN' });
        }
      },
      signOut: async () => {
        await clearStorage();
        dispatch({ type: 'SIGN_OUT' });
      },
    }),
    [],
  );

  React.useEffect(() => {
    setUpdateChats(false);
    const tryToSignIn = async () => {
      let token;
      try {
        token = await getAccessToken();
      } catch (e) {}
      if (token) {
        dispatch({ type: 'SIGN_IN' });
      }
    };

    tryToSignIn();
  }, []);

  React.useEffect(() => {
    const connectToSocket = async () => {
      const accessToken = await getAccessToken();
      if (
        accessToken &&
        state.isSignedIn &&
        (!connection || connection.state === 'Disconnected')
      ) {
        setConnection(
          new HubConnectionBuilder()
            .withUrl(`${process.env['REACT_APP_API_GATEWAY_URL']}/hubs/chat`, {
              accessTokenFactory: () => accessToken,
            })
            .withAutomaticReconnect()
            .build(),
        );
      } else if (connection) {
        await connection.stop();
        setConnection(undefined);
      }
    };

    const getUnreadChatsCount = async () => {
      const accessToken = await getAccessToken();
      if (accessToken && state.isSignedIn) {
        const unreadChats = await getUnreadNotifications(accessToken);
        if (unreadChats) {
          setUnreadChats(unreadChats?.unreadChatsCount);
        }
      }
    };

    getUnreadChatsCount();
    connectToSocket();
  }, [state.isSignedIn]);

  React.useEffect(() => {
    if (connection?.state === 'Disconnected') {
      connection.on('ReceiveMessage', async (data: MessageResponseType) => {
        setChatMessage(data);
        await setUnreadChatsCount(setUnreadChats);
        setUpdateChats(updateChatsValue);
      });

      connection.start();
    }
  }, [connection]);

  return (
    <AuthContext.Provider value={authContext}>
      <ProfileContext.Provider
        value={{
          updatePhotoUrl: async () => {
            setUpdatePhotoUrlValue(!updatePhotoUrlValue);
          },
          updatePhotoUrlValue,
          updateChats: async () => {
            setUpdateChats(!updateChatsValue);
          },
          updateChatsValue,
          updateUnreadChatsCount: async () => {
            await setUnreadChatsCount(setUnreadChats);
          },
          unreadChatsCount: unreadChats,
          chatMessage,
        }}>
        <PaperProvider>
          <NavigationContainer>
            {state.isSignedIn ? <HomeScreenStack /> : <AuthScreenStack />}
          </NavigationContainer>
        </PaperProvider>
      </ProfileContext.Provider>
    </AuthContext.Provider>
  );
};

export default App;
