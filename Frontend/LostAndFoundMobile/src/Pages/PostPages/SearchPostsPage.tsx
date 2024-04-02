import { format } from 'date-fns';
import React from 'react';
import { Picker } from '@react-native-picker/picker';
import { Pressable, Text, View } from 'react-native';
import {
  MainContainer,
  InputSection,
  CustomTextInput,
  secondary,
  light,
  dark2,
  LoadingView,
  primary,
} from '../../Components';
import {
  CategoryType,
  PublicationState,
  PublicationType,
  getCategories,
  Order,
  PublicationSortType,
} from 'commons';
import { getAccessToken } from '../../SecureStorage';
import RNDateTimePicker from '@react-native-community/datetimepicker';
import { PublicationSearchRequestType } from 'commons';
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
      textColor: primary,
    },
  });
};

export const SearchPostsPage = (props: any) => {
  const onlyUserPublications: boolean =
    props.route.params?.onlyUserPublications;
  const [show1, setShow1] = React.useState<boolean>(false);
  const [show2, setShow2] = React.useState<boolean>(false);
  const [categories, setCategories] = React.useState<CategoryType[]>([]);

  const [title, setTitle] = React.useState<string | undefined>();
  const [incidentAddress, setIncidentAddress] = React.useState<
    string | undefined
  >();
  const [distance, setDistance] = React.useState<number>();
  const [incidentFromDate, setIncidentFromDate] = React.useState<Date>(
    new Date(new Date().getTime() - 1000 * 3600 * 24 * 31 * 3),
  );
  const [incidentToDate, setIncidentToDate] = React.useState<Date>(new Date());
  const [subjectCategory, setSubjectCategory] = React.useState<CategoryType>();
  const [publicationType, setPublicationType] =
    React.useState<PublicationType>();
  const [publicationState, setPublicationState] =
    React.useState<PublicationState>();
  const [firstArgumentSort, setFirstArgumentSort] = React.useState<string>();
  const [firstArgumentSortOrder, setFirstArgumentSortOrder] =
    React.useState<Order>(Order.Ascending);
  const [loading, setLoading] = React.useState<boolean>(true);

  React.useEffect(() => {
    const getData = async () => {
      const accessToken = await getAccessToken();
      if (accessToken) {
        setCategories(await getCategories(accessToken));
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

  const onChangeFromDate = (event: any, selectedDate: Date | undefined) => {
    const currentDate = selectedDate || incidentFromDate;
    setShow1(false);
    setIncidentFromDate(currentDate);
  };

  const onChangeToDate = (event: any, selectedDate: Date | undefined) => {
    const currentDate = selectedDate || incidentToDate;
    setShow2(false);
    setIncidentToDate(currentDate);
  };

  function Search() {
    const searchPublication: PublicationSearchRequestType = {
      title: title,
      incidentAddress,
      incidentDistance: distance,
      incidentFromDate,
      incidentToDate,
      subjectCategoryId: subjectCategory?.id,
      publicationType,
      publicationState,
      onlyUserPublications,
    };
    const firstSort: PublicationSortType | undefined = firstArgumentSort
      ? { type: firstArgumentSort, order: firstArgumentSortOrder }
      : undefined;

    console.log(searchPublication);
    if (incidentFromDate > incidentToDate) {
      validationSnackBar(
        'Data końcowa wyszukiwania musi być mniejsza od początkowej',
      );
      return;
    }

    props.navigation.push('Home', {
      screen: 'Posts',
      params: {
        searchPublication: searchPublication,
        orderBy: {
          firstArgumentSort: firstSort,
        },
      },
    });
  }

  const HeaderBar = () => {
    return (
      <Appbar.Header style={{ backgroundColor: secondary }}>
        <Appbar.BackAction
          color={light}
          onPress={() => props.navigation.pop()}
        />
        <Appbar.Content
          title="Szukaj Ogłoszeń"
          titleStyle={{
            textAlign: 'center',
            color: light,
            fontWeight: 'bold',
          }}
        />
        <Appbar.Action
          size={30}
          icon="magnify"
          color={light}
          onPress={() => Search()}
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
        <InputSection title="Tytuł">
          <CustomTextInput
            onChangeText={setTitle}
            keyboardType={'default'}
            placeholder="Podaj tytuł"
          />
        </InputSection>
        <InputSection title="Lokalizacja">
          <CustomTextInput
            onChangeText={setIncidentAddress}
            keyboardType={'default'}
            placeholder="Podaj adres"
          />
        </InputSection>
        <InputSection title="Promień">
          <View style={mainStyles.pickerStyle}>
            <Picker
              selectedValue={distance}
              onValueChange={itemValue => setDistance(itemValue)}>
              <Picker.Item label="Nieograniczony" value={undefined} />
              <Picker.Item label="1 km" value={1} />
              <Picker.Item label="2 km" value={2} />
              <Picker.Item label="5 km" value={5} />
              <Picker.Item label="10 km" value={10} />
              <Picker.Item label="20 km" value={20} />
            </Picker>
          </View>
        </InputSection>
        <InputSection title="Data od">
          <Pressable onPress={() => setShow1(true)}>
            <Text
              style={{
                borderBottomWidth: 1,
                borderBottomColor: dark2,
                paddingVertical: 10,
              }}>
              {format(incidentFromDate, 'dd.MM.yyyy')}
            </Text>
            <View>
              {show1 && (
                <RNDateTimePicker
                  value={incidentFromDate}
                  mode="date"
                  is24Hour={true}
                  display="default"
                  onChange={onChangeFromDate}
                />
              )}
            </View>
          </Pressable>
        </InputSection>
        <InputSection title="Data do">
          <Pressable onPress={() => setShow2(true)}>
            <Text
              style={{
                borderBottomWidth: 1,
                borderBottomColor: dark2,
                paddingVertical: 10,
              }}>
              {format(incidentToDate, 'dd.MM.yyyy')}
            </Text>
            <View>
              {show2 && (
                <RNDateTimePicker
                  value={incidentToDate}
                  mode="date"
                  is24Hour={true}
                  display="default"
                  onChange={onChangeToDate}
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
              <Picker.Item label={'Wszystkie kategorie'} value={undefined} />
              {mapCategories}
            </Picker>
          </View>
        </InputSection>
        <InputSection title="Typ ogłoszenia">
          <View style={mainStyles.pickerStyle}>
            <Picker
              selectedValue={publicationType}
              onValueChange={itemValue => setPublicationType(itemValue)}>
              <Picker.Item label="Wszystkie typy" value={undefined} />
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
        <InputSection title="Stan ogłoszenia">
          <View style={mainStyles.pickerStyle}>
            <Picker
              selectedValue={publicationState}
              onValueChange={itemValue => setPublicationState(itemValue)}>
              <Picker.Item label="Wszystkie stany" value={undefined} />
              <Picker.Item label="Otwarte" value={PublicationState.Open} />
              <Picker.Item label="Zakończone" value={PublicationState.Closed} />
            </Picker>
          </View>
        </InputSection>
        <InputSection title="Sortuj">
          <View style={mainStyles.pickerStyle}>
            <Picker
              selectedValue={firstArgumentSort}
              onValueChange={itemValue => setFirstArgumentSort(itemValue)}>
              <Picker.Item label="Brak" value={undefined} />
              <Picker.Item label="Tytuł" value={'Title'} />
              <Picker.Item label="Kategoria" value={'SubjectCategoryId'} />
              <Picker.Item label="Data zdarzenia" value={'IncidentDate'} />
              <Picker.Item label="Średnia ocena" value={'AggregateRating'} />
              <Picker.Item label="Stan ogłoszenia" value={'PublicationState'} />
              <Picker.Item label="Typ ogłoszenia" value={'PublicationType'} />
            </Picker>
          </View>
          <View style={mainStyles.pickerStyle}>
            <Picker
              selectedValue={firstArgumentSortOrder}
              onValueChange={itemValue => setFirstArgumentSortOrder(itemValue)}>
              <Picker.Item label="Rosnąco" value={Order.Ascending} />
              <Picker.Item label="Malejąco" value={Order.Descending} />
            </Picker>
          </View>
        </InputSection>
        <View style={{ alignSelf: 'center', width: '80%', marginTop: 20 }}>
          <SecondaryButton label="Szukaj" onPress={() => Search()} />
        </View>
      </MainScrollContainer>
    </MainContainer>
  );
};
