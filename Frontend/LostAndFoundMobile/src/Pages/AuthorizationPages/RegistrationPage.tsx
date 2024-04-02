import { register, RegisterRequestType, RegisterResponseType } from 'commons';
import React from 'react';
import { Text, View } from 'react-native';
import { Appbar } from 'react-native-paper';
import Snackbar from 'react-native-snackbar';
import { light, primary, secondary } from '../../Components';
import {
  CustomTextInput,
  InputSection,
  MainButton,
  MainContainer,
  MainScrollContainer,
  PressableText,
} from '../../Components/MainComponents';

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

async function registerAccount(
  username: string,
  email: string,
  password: string,
  confirmPassword: string,
): Promise<boolean> {
  const registerRequest: RegisterRequestType = {
    username,
    email,
    password,
    confirmPassword,
  };

  if (!/\S+@\S+\.\S+/.test(email)) {
    validationSnackBar(`Wprowadziłeś niepoprawny adres e-mail`);
    return false;
  }
  if (username.length < 8) {
    validationSnackBar(
      `Długość nazwy użytkownika musi wynosić co najmniej 8 znaków`,
    );
    return false;
  }
  if (password.length < 8) {
    validationSnackBar(`Długość hasła musi wynosić co najmniej 8 znaków`);
    return false;
  }
  if (password !== confirmPassword) {
    validationSnackBar(`Wprowadzone hasła nie są jednakowe`);
    return false;
  }

  const registerResponse = await register(registerRequest);
  if (registerResponse.ok) {
    return true;
  } else {
    if (
      registerResponse.errors?.Email &&
      registerResponse.errors?.Email.length > 0
    ) {
      validationSnackBar(registerResponse.errors.Email[0]);
    } else if (
      registerResponse.errors?.Username &&
      registerResponse.errors?.Username.length > 0
    ) {
      validationSnackBar(registerResponse.errors.Username[0]);
    } else {
      validationSnackBar(`Coś poszło nie tak, spróbuj ponownie`);
    }
    return false;
  }
}

export const RegistrationPage = (props: { navigation: string[] }) => {
  const [email, setEmail] = React.useState<string>('');
  const [username, setUsername] = React.useState<string>('');
  const [password, setPassword] = React.useState<string>('');
  const [confirmPassword, setConfirmPassword] = React.useState<string>('');

  const onEmailChange = (email: string) => {
    setEmail(email);
  };

  const onUsernameChange = (email: string) => {
    setUsername(email);
  };

  const onPasswordChange = (password: string) => {
    setPassword(password);
  };

  const onConfirmPasswordChange = (password: string) => {
    setConfirmPassword(password);
  };

  return (
    <MainContainer>
      <Appbar.Header style={{ backgroundColor: secondary }}>
        <Appbar.BackAction
          color={light}
          onPress={() => props.navigation.pop()}
        />
        <Appbar.Content
          title="Zarejestruj się"
          titleStyle={{
            textAlign: 'center',
            color: light,
            fontWeight: 'bold',
          }}
        />
        <Appbar.Action icon="flask-empty" color={secondary}></Appbar.Action>
      </Appbar.Header>
      <MainScrollContainer>
        <InputSection title="E-mail">
          <CustomTextInput
            testID="emailPlaceholder"
            onChangeText={onEmailChange}
            keyboardType={'email-address'}
            placeholder="Podaj swój adres e-mail"
          />
        </InputSection>
        <InputSection title="Nazwa użytkownika">
          <CustomTextInput
            testID="usernamePlaceholder"
            onChangeText={onUsernameChange}
            keyboardType={'default'}
            placeholder="Podaj swoją nazwę użytkownika"
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
        <InputSection title="Powtórz hasło">
          <CustomTextInput
            testID="confirmPasswordPlaceholder"
            onChangeText={onConfirmPasswordChange}
            secureTextEntry={true}
            keyboardType={'default'}
            placeholder="********"
          />
        </InputSection>
        <MainButton
          testID="registerButton"
          label="Zarejestruj się"
          onPress={async () => {
            const isRegistered = await registerAccount(
              username,
              email,
              password,
              confirmPassword,
            );
            if (isRegistered) {
              props.navigation.push('Login');
            }
          }}
        />
        <View style={{ alignItems: 'center' }}>
          <Text>Masz konto?</Text>
          <PressableText
            testID="loginButton"
            text="Zaloguj się"
            onPress={() => props.navigation.push('Login')}
          />
        </View>
      </MainScrollContainer>
    </MainContainer>
  );
};
