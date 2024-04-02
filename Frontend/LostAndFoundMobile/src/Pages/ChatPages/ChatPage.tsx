import React from 'react';
import { Pressable, StyleSheet, Text, TextInput, View } from 'react-native';
import { KeyboardAwareFlatList } from 'react-native-keyboard-aware-scroll-view';
import {
  dark,
  dark2,
  light,
  light3,
  LoadingView,
  MainContainer,
  primary,
  secondary,
} from '../../Components';
import {
  addChatMessage,
  BaseProfileType,
  getChatMessages,
  MessageRequestType,
  MessageResponseType,
  readChat,
} from 'commons';
import { getAccessToken, getUserId } from '../../SecureStorage';
import { Appbar, Avatar } from 'react-native-paper';
import { ProfileContext } from '../../Context';
import { PaginationMetadata } from 'commons/lib/http';
import { RefreshControl } from 'react-native-gesture-handler';
import { dark3 } from '../../Components/Colors';

const GetMessages = async (
  recipentId: string,
  accessToken: string,
  pageNumber: number,
): Promise<{
  pagination?: PaginationMetadata;
  messages: MessageResponseType[];
}> => await getChatMessages(recipentId, accessToken, pageNumber);

const SendMessage = async (
  recipentId: string,
  message: MessageRequestType,
  accessToken: string,
) => await addChatMessage(recipentId, message, accessToken);

const ReadChat = async (chatRecipent: BaseProfileType | undefined) => {
  const accessToken = await getAccessToken();
  if (chatRecipent && accessToken) {
    await readChat(chatRecipent.userId, accessToken);
  }
};

const MessageItem = (props: any) => {
  const currentUserId: string = props.currentUserId;
  const message: MessageResponseType = props.item;
  return (
    <View
      style={[
        styles.message,
        String(message.authorId) !== String(currentUserId)
          ? styles.messageLeft
          : styles.messageRight,
      ]}>
      <Text
        style={[
          styles.messageText,
          String(message.authorId) !== String(currentUserId)
            ? styles.messageTextLeft
            : styles.messageTextRight,
        ]}>
        {message.content}
      </Text>
    </View>
  );
};

export const ChatPage = (props: any) => {
  const chatRecipent: BaseProfileType = props.route.params?.chatRecipent;
  const { updateChats, updateUnreadChatsCount, updateChatsValue, chatMessage } =
    React.useContext(ProfileContext);
  const [messageContent, setMessageContent] = React.useState<string>('');
  const [currentUserId, setCurrentUserId] = React.useState<string | null>();
  const [messagesData, setMessagesData] = React.useState<MessageResponseType[]>(
    [],
  );
  const [flatListRef, setFlatListRef] =
    React.useState<KeyboardAwareFlatList | null>(null);
  const [pageNumber, setPageNumber] = React.useState<number>(1);
  const [pagination, setPagination] = React.useState<PaginationMetadata>();
  const [loading, setLoading] = React.useState<boolean>(true);
  const [receiveMessage, setReceiveMessage] = React.useState<boolean>(false);

  React.useEffect(() => {
    const getData = async () => {
      setCurrentUserId(await getUserId());
      await ReadChat(chatRecipent);
      updateChats();
      updateUnreadChatsCount();
    };
    getData();
  }, []);

  React.useEffect(() => {
    const getData = async () => {
      const accessToken = await getAccessToken();
      if (accessToken) {
        const responseData = await GetMessages(
          chatRecipent?.userId,
          accessToken,
          1,
        );
        setMessagesData(responseData.messages);
        setPagination(responseData.pagination);
        setLoading(false);
        await ReadChat(chatRecipent);
        updateChats();
        updateUnreadChatsCount();
      }
    };

    getData();
  }, []);

  React.useEffect(() => {
    const getMessages = async () => {
      if (
        chatMessage &&
        receiveMessage &&
        (chatMessage.authorId === chatRecipent.userId ||
          chatMessage.authorId === currentUserId)
      ) {
        setMessagesData([chatMessage, ...messagesData]);
        flatListRef?.scrollToPosition(-1, -1);
      } else {
        setReceiveMessage(true);
      }
    };

    getMessages();
  }, [chatMessage]);

  const onRefresh = React.useCallback(async () => {
    await ReadChat(chatRecipent);
    updateChats();
    updateUnreadChatsCount();
  }, []);

  const HeaderBar = () => {
    return (
      <Appbar.Header style={{ backgroundColor: secondary }}>
        <Appbar.BackAction
          color={light}
          onPress={() => props.navigation.pop()}
        />
        <Appbar.Content
          title={<Text>{chatRecipent?.username}</Text>}
          titleStyle={{
            textAlign: 'center',
            color: light,
            fontWeight: 'bold',
          }}
        />
        <Pressable
          onPress={() => {
            props.navigation.push('Home', {
              screen: 'Profile',
              params: { userId: chatRecipent?.userId },
            });
          }}>
          {chatRecipent?.pictureUrl ? (
            <Avatar.Image
              source={{
                uri: chatRecipent?.pictureUrl,
              }}
              style={{
                alignSelf: 'center',
                marginRight: 30,
                backgroundColor: light3,
              }}
              size={40}
            />
          ) : (
            <Avatar.Icon
              icon={'account'}
              size={40}
              style={{
                alignSelf: 'center',
                marginRight: 30,
                backgroundColor: light3,
              }}
            />
          )}
        </Pressable>
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
      <KeyboardAwareFlatList
        inverted={true}
        style={{ padding: 30, marginBottom: 70, flex: 1 }}
        contentContainerStyle={{ paddingBottom: 50 }}
        data={messagesData}
        renderItem={({ item }) => (
          <MessageItem item={item} currentUserId={currentUserId} />
        )}
        ref={setFlatListRef}
        onKeyboardDidShow={async () => {
          flatListRef?.scrollToPosition(-1, -1);
          await ReadChat(chatRecipent);
          updateChats();
          updateUnreadChatsCount();
        }}
        onEndReached={() => {
          const getData = async () => {
            if (pagination && pageNumber < pagination?.TotalPageCount) {
              const accessToken = await getAccessToken();
              if (accessToken) {
                const responseData = await GetMessages(
                  chatRecipent.userId,
                  accessToken,
                  pageNumber + 1,
                );
                setMessagesData([...messagesData, ...responseData.messages]);
                setPagination(responseData.pagination);
                setPageNumber(pageNumber + 1);
              }
            }
          };
          getData();
        }}
        refreshControl={
          <RefreshControl
            refreshing={false}
            onRefresh={onRefresh}
            colors={[primary]}
          />
        }
      />
      <View style={styles.sendMessageContainer}>
        <TextInput
          multiline={true}
          onChangeText={setMessageContent}
          value={messageContent}
          placeholder="Podaj tytuł"
          style={styles.inputMessage}
        />

        <Pressable
          style={({ pressed }) => [
            styles.sendButton,
            pressed ? { backgroundColor: dark3 } : {},
          ]}
          onPress={async () => {
            const accessToken = await getAccessToken();
            if (accessToken && messageContent.length > 0) {
              const message: MessageRequestType = {
                content: messageContent,
              };
              const response = await SendMessage(
                chatRecipent?.userId,
                message,
                accessToken,
              );
              if (response) {
                setMessagesData([response, ...messagesData]);
                setMessageContent('');
                flatListRef?.scrollToPosition(-1, -1);
              }
            }
          }}>
          <Text style={{ color: light, fontWeight: '600', fontSize: 18 }}>
            Wyślij
          </Text>
        </Pressable>
      </View>
    </MainContainer>
  );
};

const styles = StyleSheet.create({
  sendMessageContainer: {
    flexDirection: 'row',
    justifyContent: 'space-between',
    position: 'absolute',
    bottom: 0,
    backgroundColor: secondary,
    padding: 10,
  },
  inputMessage: {
    borderRadius: 20,
    borderWidth: 1,
    flex: 4,
    marginRight: 10,
    paddingLeft: 4,
    backgroundColor: light,
  },
  sendButton: {
    borderRadius: 20,
    backgroundColor: primary,
    justifyContent: 'center',
    alignItems: 'center',
    alignSelf: 'center',
    padding: 10,
    paddingVertical: 12,
    flex: 1,
  },
  message: {
    borderWidth: 1,
    borderRadius: 20,
    borderColor: dark2,
    padding: 10,
    marginBottom: 10,
    maxWidth: '70%',
  },
  messageLeft: {
    alignSelf: 'flex-start',
    backgroundColor: light,
  },
  messageRight: {
    alignSelf: 'flex-end',
    backgroundColor: secondary,
  },
  messageText: {},
  messageTextLeft: {
    color: dark,
  },
  messageTextRight: {
    color: light,
  },
});
