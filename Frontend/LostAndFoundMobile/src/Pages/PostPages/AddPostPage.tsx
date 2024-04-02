import RNDateTimePicker from '@react-native-community/datetimepicker';
import { Picker } from '@react-native-picker/picker';
import {
  addPublication,
  CategoryType,
  getCategories,
  PublicationRequestType,
  PublicationResponseType,
  PublicationType,
} from 'commons';
import { format } from 'date-fns';
import React from 'react';
import { Pressable, Text, View } from 'react-native';
import {
  CustomTextInput,
  DocumentSelector,
  InputSection,
  light,
  light2,
  LoadingView,
  MainContainer,
  secondary,
} from '../../Components';
import { getAccessToken } from '../../SecureStorage';
import { DocumentPickerResponse } from 'react-native-document-picker';
import { Appbar } from 'react-native-paper';
import {
  MainScrollContainer,
  mainStyles,
  SecondaryButton,
} from '../../Components/MainComponents';
import Snackbar from 'react-native-snackbar';

const validationSnackBar = (text: string) => {
  Snackbar.show({
    text,
    duration: Snackbar.LENGTH_LONG,
    action: {
      text: 'Zamknij',
      textColor: 'green',
    },
  });
};

const addNewPost = async (
  publication: PublicationRequestType,
  photo?: DocumentPickerResponse,
): Promise<PublicationResponseType | undefined> => {
  if (!publication.title || !/\S+/.test(publication.title)) {
    validationSnackBar(`Tytuł nie może być pusty`);
    return undefined;
  }
  if (
    !publication.incidentAddress ||
    !/\S+/.test(publication.incidentAddress)
  ) {
    validationSnackBar(`Lokalizacja nie może być pusta`);
    return undefined;
  }
  if (publication.incidentDate > new Date()) {
    validationSnackBar(
      `Podana data musi być mniejsza niż ${new Date().toLocaleDateString()}`,
    );
    return undefined;
  }
  if (!publication.description || !/\S+/.test(publication.description)) {
    validationSnackBar(`Opis nie może być pusty`);
    return undefined;
  }

  const accessToken = await getAccessToken();
  if (accessToken) {
    if (photo) {
      const photoRequest = {
        name: photo.name,
        type: photo.type,
        uri: photo.uri,
        size: photo.size,
      };
      return await addPublication(publication, accessToken, photoRequest);
    }
    return await addPublication(publication, accessToken);
  }
};

export const AddPostPage = (props: any) => {
  const [show, setShow] = React.useState<boolean>(false);
  const [categories, setCategories] = React.useState<CategoryType[]>([]);

  const [fileResponse, setFileResponse] = React.useState<
    DocumentPickerResponse[]
  >([]);
  const [title, setTitle] = React.useState<string>('');
  const [description, setDescription] = React.useState<string>('');
  const [incidentAddress, setIncidentAddress] = React.useState<string>('');
  const [incidentDate, setIncidentDate] = React.useState<Date>(new Date());
  const [subjectCategory, setSubjectCategory] = React.useState<
    CategoryType | undefined
  >({ id: 'Other', displayName: 'Inne' });
  const [publicationType, setPublicationType] = React.useState<PublicationType>(
    PublicationType.LostSubject,
  );
  const [loading, setLoading] = React.useState<boolean>(true);

  React.useEffect(() => {
    const getData = async () => {
      const accessToken = await getAccessToken();
      if (accessToken) {
        setCategories(await getCategories(accessToken));
        if (categories.length > 0) setSubjectCategory(categories[0]);
        setLoading(false);
      }
    };

    getData();
  }, []);

  const mapCategories = categories.map(category => {
    return (
      <Picker.Item
        key={category.id}
        label={category.displayName}
        value={category}
      />
    );
  });

  const onChange = (event: any, selectedDate: Date | undefined) => {
    const currentDate = selectedDate || incidentDate;
    setShow(false);
    setIncidentDate(currentDate);
  };

  async function AddPost() {
    const publication: PublicationRequestType = {
      title,
      description,
      incidentAddress,
      incidentDate,
      subjectCategoryId: subjectCategory?.id,
      publicationType,
    };

    const response = await addNewPost(
      publication,
      fileResponse.length > 0 ? fileResponse[0] : undefined,
    );
    if (response) {
      props.navigation.push('Home', {
        screen: 'Post',
        params: { publicationId: response?.publicationId },
      });
    }
  }

  const HeaderBar = () => {
    return (
      <Appbar.Header style={{ backgroundColor: secondary }}>
        <Appbar.BackAction
          color={light}
          onPress={() => props.navigation.pop()}
        />
        <Appbar.Content
          title="Stwórz Ogłoszenie"
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
          onPress={async () => await AddPost()}
        />
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
      <MainScrollContainer>
        <DocumentSelector
          fileResponse={fileResponse}
          setFileResponse={setFileResponse}
          label="Dodaj zdjęcie"
        />

        <InputSection title="Tytuł Ogłoszenia">
          <CustomTextInput
            testID="titlePlaceholder"
            onChangeText={setTitle}
            keyboardType={'default'}
            value={title}
          />
        </InputSection>
        <InputSection title="Lokalizacja">
          <CustomTextInput
            onChangeText={setIncidentAddress}
            keyboardType={'default'}
            value={incidentAddress}
          />
        </InputSection>
        <InputSection title="Data">
          <Pressable onPress={() => setShow(true)}>
            <Text
              style={{
                borderBottomWidth: 1,
                borderBottomColor: light2,
                paddingVertical: 10,
              }}>
              {format(incidentDate, 'dd.MM.yyyy')}
            </Text>
            <View>
              {show && (
                <RNDateTimePicker
                  value={incidentDate}
                  mode="date"
                  is24Hour={true}
                  display="default"
                  onChange={onChange}
                />
              )}
            </View>
          </Pressable>
        </InputSection>
        <InputSection title="Kategoria">
          <View style={mainStyles.pickerStyle}>
            <Picker
              selectedValue={subjectCategory}
              onValueChange={setSubjectCategory}>
              {mapCategories}
            </Picker>
          </View>
        </InputSection>
        <InputSection title="Typ ogłoszenia">
          <View style={mainStyles.pickerStyle}>
            <Picker
              selectedValue={publicationType}
              onValueChange={itemValue => setPublicationType(itemValue)}>
              <Picker.Item
                label="Zgubione"
                value={PublicationType.LostSubject}
              />
              <Picker.Item
                label="Znalezione"
                value={PublicationType.FoundSubject}
              />
            </Picker>
          </View>
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
            label="Dodaj ogłoszenie"
            onPress={async () => await AddPost()}
          />
        </View>
      </MainScrollContainer>
    </MainContainer>
  );
};
