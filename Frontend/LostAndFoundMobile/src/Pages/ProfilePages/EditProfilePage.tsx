import {
  editProfile,
  editProfilePhoto,
  ProfileRequestType,
  ProfileResponseType,
} from 'commons';
import React from 'react';
import { DocumentPickerResponse } from 'react-native-document-picker';
import {
  CustomTextInput,
  DocumentSelector,
  InputSection,
  light,
  MainContainer,
  secondary,
} from '../../Components';
import {
  getAccessToken,
  removeName,
  removeSurname,
  removeUsername,
  removeUserPhotoUrl,
  saveName,
  saveSurname,
  saveUsername,
  saveUserPhotoUrl,
  saveUserRating,
} from '../../SecureStorage';
import {
  MainScrollContainer,
  SecondaryButton,
} from '../../Components/MainComponents';
import { Appbar } from 'react-native-paper';
import { View } from 'react-native';
import { ProfileContext } from '../../Context';

const editProfileDetails = async (profile: ProfileRequestType) => {
  const accessToken = await getAccessToken();
  if (accessToken) {
    const response = await editProfile(profile, accessToken);
    if (response) {
      await saveUserRating(response.averageProfileRating.toString());
      if (response?.username) await saveUsername(response.username);
      else await removeUsername();
      if (response?.name) await saveName(response.name);
      else await removeName();
      if (response?.surname) await saveSurname(response.surname);
      else await removeSurname();
    }
    return response;
  }
};

const updateProfilePhoto = async (photo: DocumentPickerResponse) => {
  const accessToken = await getAccessToken();
  if (accessToken) {
    const photoRequest = {
      name: photo.name,
      type: photo.type,
      uri: photo.uri,
      size: photo.size,
    };
    const response = await editProfilePhoto(photoRequest, accessToken);
    return response;
  }
};

export const EditProfilePage = (props: any) => {
  const { updatePhotoUrl } = React.useContext(ProfileContext);
  const user: ProfileResponseType = props.route.params.user;
  const [fileResponse, setFileResponse] = React.useState<
    DocumentPickerResponse[]
  >([]);
  const [name, setName] = React.useState<string | undefined>(user.name);
  const [surname, setSurname] = React.useState<string | undefined>(
    user.surname,
  );
  const [city, setCity] = React.useState<string | undefined>(user.city);
  const [description, setDescription] = React.useState<string | undefined>(
    user.description,
  );

  async function SaveChanges() {
    const profile: ProfileRequestType = {
      name,
      surname,
      city,
      description,
    };

    if (fileResponse.length > 0) {
      const updatePhotoResponse = await updateProfilePhoto(fileResponse[0]);
      if (updatePhotoResponse) {
        if (updatePhotoResponse.pictureUrl) {
          await saveUserPhotoUrl(updatePhotoResponse.pictureUrl);
        } else {
          await removeUserPhotoUrl();
        }
        await updatePhotoUrl();
      }
    }

    const response = await editProfileDetails(profile);
    if (response) {
      props.navigation.push('Home', { screen: 'ProfileMe' });
    }
  }

  return (
    <MainContainer>
      <Appbar.Header style={{ backgroundColor: secondary }}>
        <Appbar.BackAction
          color={light}
          onPress={() => props.navigation.pop()}
        />
        <Appbar.Content
          title="Edytuj Profil"
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
          onPress={async () => await SaveChanges()}
        />
      </Appbar.Header>
      <MainScrollContainer>
        <DocumentSelector
          fileResponse={fileResponse}
          setFileResponse={setFileResponse}
          label="Ustaw zdjęcie profilowe"
        />
        <InputSection title="Imię">
          <CustomTextInput
            onChangeText={setName}
            keyboardType={'default'}
            value={name}
          />
        </InputSection>
        <InputSection title="Nazwisko">
          <CustomTextInput
            onChangeText={setSurname}
            keyboardType={'default'}
            value={surname}
          />
        </InputSection>

        <InputSection title="Miasto">
          <CustomTextInput
            onChangeText={setCity}
            keyboardType={'default'}
            value={city}
          />
        </InputSection>
        <InputSection title="Opis">
          <CustomTextInput
            onChangeText={setDescription}
            keyboardType={'default'}
            value={description}
          />
        </InputSection>
        <View style={{ alignSelf: 'center', width: '80%', marginTop: 20 }}>
          <SecondaryButton
            label="Zapisz zmiany"
            onPress={async () => await SaveChanges()}
          />
        </View>
      </MainScrollContainer>
    </MainContainer>
  );
};
