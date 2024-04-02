import { changePwd, EditPwdRequestType } from 'commons';
import React from 'react';
import { Appbar } from 'react-native-paper';
import Snackbar from 'react-native-snackbar';
import { light, primary, secondary } from '../../Components';
import {
  CustomTextInput,
  InputSection,
  MainButton,
  MainContainer,
  MainScrollContainer,
} from '../../Components/MainComponents';
import { getAccessToken } from '../../SecureStorage';

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

async function changeUserPassword(
  currentPassword: string,
  newPassword: string,
  confirmNewPassword: string,
): Promise<boolean> {
  if (newPassword.length < 8) {
    validationSnackBar(
      `Długość nowego hasła musi wynosić co najmniej 8 znaków`,
    );
    return false;
  }
  if (newPassword !== confirmNewPassword) {
    validationSnackBar(`Wprowadzone nowe hasło nie jest jednakowe`);
    return false;
  }

  const changePasswordRequest: EditPwdRequestType = {
    Password: currentPassword,
    NewPassword: newPassword,
  };
  const accessToken = await getAccessToken();
  if (accessToken) {
    const registerResponse = await changePwd(
      accessToken,
      changePasswordRequest,
    );
    if (registerResponse) {
      return true;
    } else {
      validationSnackBar(`Wprowadzono niepoprawne hasło`);
    }
  }
  return false;
}

export const EditPasswordPage = (props: { navigation: string[] }) => {
  const [currentPassword, setCurrentPassword] = React.useState<string>('');
  const [newPassword, setNewPassword] = React.useState<string>('');
  const [confirmNewPassword, setConfirmNewPassword] =
    React.useState<string>('');

  return (
    <MainContainer>
      <Appbar.Header style={{ backgroundColor: secondary }}>
        <Appbar.BackAction
          color={light}
          onPress={() => props.navigation.pop()}
        />
        <Appbar.Content
          title="Zmiana hasła"
          titleStyle={{
            textAlign: 'center',
            color: light,
            fontWeight: 'bold',
          }}
        />
        <Appbar.Action
          size={30}
          icon="content-save"
          color={light}
          onPress={async () => {
            const isPasswordChanged = await changeUserPassword(
              currentPassword,
              newPassword,
              confirmNewPassword,
            );
            if (isPasswordChanged) {
              props.navigation.push('ProfileMe');
            }
          }}
        />
      </Appbar.Header>
      <MainScrollContainer>
        <InputSection title="Obecne hasło">
          <CustomTextInput
            onChangeText={setCurrentPassword}
            secureTextEntry={true}
            keyboardType={'default'}
            placeholder="********"
          />
        </InputSection>
        <InputSection title="Nowe hasło">
          <CustomTextInput
            onChangeText={setNewPassword}
            secureTextEntry={true}
            keyboardType={'default'}
            placeholder="********"
          />
        </InputSection>
        <InputSection title="Powtórz nowe hasło">
          <CustomTextInput
            onChangeText={setConfirmNewPassword}
            secureTextEntry={true}
            keyboardType={'default'}
            placeholder="********"
          />
        </InputSection>
        <MainButton
          label="Zmień hasło"
          onPress={async () => {
            const isPasswordChanged = await changeUserPassword(
              currentPassword,
              newPassword,
              confirmNewPassword,
            );
            if (isPasswordChanged) {
              props.navigation.push('ProfileMe');
            }
          }}
        />
      </MainScrollContainer>
    </MainContainer>
  );
};
