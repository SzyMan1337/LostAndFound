import { createNativeStackNavigator } from '@react-navigation/native-stack';
import { DrawerScreenStack } from './DrawerStack';
import {
  ChatPage,
  ChatsPage,
  PostPage,
  PostsPage,
  ProfilePage,
  ProfilePageMe,
  EditProfilePage,
  SearchPostsPage,
  AddPostPage,
  EditPostPage,
  EditPasswordPage,
} from '../Pages';

const HomeStack = createNativeStackNavigator();
export function HomeScreenStack() {
  return (
    <HomeStack.Navigator screenOptions={{ headerShown: false }}>
      <HomeStack.Screen
        name="Home"
        component={DrawerScreenStack}
        options={{ headerShown: false }}
      />
      <HomeStack.Screen name="Profile" component={ProfilePage} />
      <HomeStack.Screen name="ProfileMe" component={ProfilePageMe} />
      <HomeStack.Screen name="EditProfile" component={EditProfilePage} />
      <HomeStack.Screen name="EditPassword" component={EditPasswordPage} />
      <HomeStack.Screen name="AddPost" component={AddPostPage} />
      <HomeStack.Screen name="EditPost" component={EditPostPage} />
      <HomeStack.Screen name="Posts" component={PostsPage} />
      <HomeStack.Screen name="Post" component={PostPage} />
      <HomeStack.Screen name="SearchPosts" component={SearchPostsPage} />
      <HomeStack.Screen name="Chats" component={ChatsPage} />
      <HomeStack.Screen name="Chat" component={ChatPage} />
    </HomeStack.Navigator>
  );
}
