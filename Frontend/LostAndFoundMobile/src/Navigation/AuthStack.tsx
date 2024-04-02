import { createNativeStackNavigator } from '@react-navigation/native-stack';
import React from 'react';
import { LoginPage, RegistrationPage } from '../Pages';

const AuthStack = createNativeStackNavigator();
export const AuthScreenStack = (props: any) => {
  return (
    <AuthStack.Navigator screenOptions={{ headerShown: false }}>
      <AuthStack.Screen name="Login" component={LoginPage} />
      <AuthStack.Screen name="Registration" component={RegistrationPage} />
    </AuthStack.Navigator>
  );
};
