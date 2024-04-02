import { LoginRequestType, getProfile, login } from 'commons';
import React from 'react';
import { Image, Text, View } from 'react-native';
import Snackbar from 'react-native-snackbar';
import { AuthContext } from '../../Context';
import {
  CustomTextInput,
  InputSection,
  MainButton,
  MainContainer,
  MainScrollContainer,
  PressableText,
  Subtitle,
} from '../../Components/MainComponents';
import { Logo } from '../../Images';
import {
  saveAccessToken,
  saveRefreshToken,
  saveUserId,
} from '../../SecureStorage';
import {
  saveName,
  saveSurname,
  saveUsername,
  saveUserPhotoUrl,
  saveUserRating,
} from '../../SecureStorage/Profile';
import { primary } from '../../Components';

const loginUser = async (email: string, password: string) => {
  const loginRequest: LoginRequestType = {
    email,
    password,
  };

  const loginResponse = await login(loginRequest);
  if (loginResponse) {
    await saveAccessToken(
      loginResponse.accessToken,
      loginResponse.accessTokenExpirationTime,
    );
    await saveRefreshToken(loginResponse.refreshToken);
    const profile = await getProfile(loginResponse.accessToken);
    if (profile) {
      await saveUserId(profile.userId);
      await saveUserRating(profile.averageProfileRating.toString());
      if (profile.username) await saveUsername(profile.username);
      if (profile.name) await saveName(profile.name);
      if (profile.surname) await saveSurname(profile.surname);
      if (profile.pictureUrl) await saveUserPhotoUrl(profile.pictureUrl);
    }
  } else {
    Snackbar.show({
      text: 'Niepoprawny adres e-mail lub hasło',
      duration: Snackbar.LENGTH_SHORT,
      action: {
        text: 'Zamknij',
        textColor: primary,
      },
    });
    return;
  }
};

export const LoginPage = (props: { navigation: string[] }) => {
  const [email, setEmail] = React.useState<string>('');
  const [password, setPassword] = React.useState<string>('');
  const { signIn } = React.useContext(AuthContext);

  const onUsernameChange = (email: string) => {
    setEmail(email);
  };

  const onPasswordChange = (password: string) => {
    setPassword(password);
  };

  return (
    <MainContainer>
      <MainScrollContainer>
        <Image
          source={Logo}
          style={{ width: '100%', resizeMode: 'stretch', marginBottom: 20 }}
        />
        <Subtitle>Hej! Dobrze Cię znowu widzieć</Subtitle>
        <InputSection title="E-mail">
          <CustomTextInput
            testID="emailPlaceholder"
            onChangeText={onUsernameChange}
            keyboardType={'email-address'}
            placeholder="Podaj swój adres e-mail"
          />
        </InputSection>
        <InputSection title="Hasło">
          <CustomTextInput
            testID="passwordPlaceholder"
            onChangeText={onPasswordChange}
            secureTextEntry={true}
            keyboardType={'default'}
            placeholder="********"
          />
        </InputSection>
        <MainButton
          testID="loginButton"
          label="Zaloguj się"
          onPress={async () => {
            await loginUser(email, password);
            await signIn();
          }}
        />
        <View style={{ alignItems: 'center' }}>
          <Text>Nie masz konta?</Text>
          <PressableText
            testID="registerButton"
            text="Zarejestruj się"
            onPress={() => props.navigation.push('Registration')}
          />
        </View>
      </MainScrollContainer>
    </MainContainer>
  );
};
