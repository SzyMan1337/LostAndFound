import React from 'react';
import {
  ActivityIndicator,
  Dimensions,
  FlatList,
  Image,
  Pressable,
  StyleSheet,
  Text,
  TouchableOpacity,
  View,
} from 'react-native';
import MaterialIconsIcon from 'react-native-vector-icons/MaterialIcons';
import { format } from 'date-fns';
import {
  danger,
  dark,
  dark2,
  light,
  light2,
  LoadingNextPageView,
  LoadingView,
  MainContainer,
  primary,
  secondary,
  success,
} from '../../Components';
import { getPublications, PublicationResponseType } from 'commons';
import { getAccessToken } from '../../SecureStorage';
import { PublicationSearchRequestType } from 'commons';
import IoniconsIcon from 'react-native-vector-icons/Ionicons';
import { Appbar } from 'react-native-paper';
import { PaginationMetadata } from 'commons/lib/http';

const PostItem = (props: any) => {
  const item: PublicationResponseType = props.item;
  const incidentDate = format(item.incidentDate, 'dd.MM.yyyy');
  const [width, setWidth] = React.useState<number>(10);
  const [imageDisplayedSize, setImageDisplayedSize] = React.useState<{
    width: number;
    height: number;
  }>();

  React.useEffect(() => {
    if (item?.subjectPhotoUrl) {
      let imageSize = { width: 100, height: 100 };
      Image.getSize(
        item.subjectPhotoUrl,
        (width, height) => (imageSize = { width, height }),
      );
      const displayedWidth = width - 20;
      const displayedHeight =
        (imageSize.height * displayedWidth) / imageSize.width;
      setImageDisplayedSize({
        width: displayedWidth,
        height: displayedHeight,
      });
    }
  }, [item, width]);

  return (
    <Pressable
      onLayout={event => {
        setWidth(event.nativeEvent.layout.width);
      }}
      onPress={props.onPress}
      style={styles.postItem}>
      <View>
        <Text style={{ fontSize: 20, fontWeight: '600', color: dark }}>
          {item.title}
        </Text>
        <View
          style={{
            flexDirection: 'row',
            justifyContent: 'space-between',
          }}>
          <Text style={{ fontSize: 16, color: dark2 }}>{incidentDate}</Text>
          <Text
            style={
              item.aggregateRating >= 0
                ? styles.positiveScore
                : styles.negativeScore
            }>
            {item.aggregateRating > 0
              ? `+${item.aggregateRating}`
              : item.aggregateRating}
          </Text>
        </View>
        <Text numberOfLines={3} style={{ color: dark2 }}>
          {item.description}
        </Text>
      </View>
      <View>
        {item?.subjectPhotoUrl ? (
          <Image
            source={{
              uri: item?.subjectPhotoUrl,
            }}
            style={{
              width: imageDisplayedSize?.width
                ? imageDisplayedSize.width
                : width - 20,
              height: imageDisplayedSize?.height
                ? imageDisplayedSize.height
                : 100,
            }}
          />
        ) : (
          <MaterialIconsIcon name="add-a-photo" size={width - 20} />
        )}
      </View>
    </Pressable>
  );
};

export const PostsPage = (props: any) => {
  const searchPublication: PublicationSearchRequestType =
    props.route.params?.searchPublication;
  const orderBy = props.route.params?.orderBy;
  const [postsData, setPostsData] = React.useState<PublicationResponseType[]>(
    [],
  );
  const [pageNumber, setPageNumber] = React.useState<number>(1);
  const [pagination, setPagination] = React.useState<PaginationMetadata>();
  const [loading, setLoading] = React.useState<boolean>(true);
  const [loadingNextPage, setLoadingNextPage] = React.useState<boolean>(false);

  React.useEffect(() => {
    const getData = async () => {
      const accessToken = await getAccessToken();
      if (accessToken) {
        const responseData = await getPublications(
          1,
          accessToken,
          searchPublication,
          orderBy,
        );
        setPostsData(responseData.publications);
        setPagination(responseData.pagination);
        setLoading(false);
      }
    };

    getData();
  }, [searchPublication, orderBy]);

  const win = Dimensions.get('window');
  const buttonStyles = StyleSheet.create({
    circle: {
      backgroundColor: primary,
      width: 70,
      height: 70,
      position: 'absolute',
      bottom: 30,
      right: 0.5 * win.width - 35,
      borderRadius: 50,
      justifyContent: 'center',
      alignItems: 'center',
    },
  });

  const HeaderBar = () => {
    return (
      <Appbar.Header style={{ backgroundColor: secondary }}>
        <Appbar.Action icon="flask-empty" color={secondary}></Appbar.Action>
        <Appbar.Content
          title={
            searchPublication?.onlyUserPublications
              ? 'Moje Ogłoszenia'
              : 'Ogłoszenia'
          }
          titleStyle={{
            textAlign: 'center',
            color: light,
            fontWeight: 'bold',
          }}
        />
        <Appbar.Action
          size={30}
          icon="archive-search-outline"
          color={light}
          onPress={() =>
            props.navigation.push('Home', {
              screen: 'SearchPosts',
              params: {
                onlyUserPublications: searchPublication?.onlyUserPublications,
              },
            })
          }
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
      <FlatList
        style={{ padding: 30 }}
        data={postsData}
        numColumns={2}
        columnWrapperStyle={{
          justifyContent: 'space-between',
          marginBottom: 15,
        }}
        contentContainerStyle={{ paddingBottom: 150 }}
        keyExtractor={item => item.publicationId}
        renderItem={({ item }) => (
          <PostItem
            item={item}
            onPress={() =>
              props.navigation.push('Home', {
                screen: 'Post',
                params: { publicationId: item.publicationId },
              })
            }
          />
        )}
        onEndReached={() => {
          const getData = async () => {
            setLoadingNextPage(true);
            if (pagination && pageNumber < pagination?.TotalPageCount) {
              const accessToken = await getAccessToken();
              if (accessToken) {
                const responseData = await getPublications(
                  pageNumber + 1,
                  accessToken,
                  searchPublication,
                  orderBy,
                );
                setPostsData([...postsData, ...responseData.publications]);
                setPagination(responseData.pagination);
                setPageNumber(pageNumber + 1);
              }
            }
            setLoadingNextPage(false);
          };
          getData();
        }}
        ListFooterComponent={() => {
          return (
            <View style={{ marginBottom: 10 }}>
              {loadingNextPage ? <LoadingNextPageView /> : <></>}
            </View>
          );
        }}
      />
      <TouchableOpacity
        activeOpacity={0.7}
        style={buttonStyles.circle}
        testID="AddPostButton"
        onPress={() => {
          props.navigation.push('Home', {
            screen: 'AddPost',
          });
        }}>
        <IoniconsIcon name="add" size={55} color={light} />
      </TouchableOpacity>
    </MainContainer>
  );
};

const styles = StyleSheet.create({
  pageContainer: {
    flex: 1,
  },
  titleSection: {
    flexDirection: 'row',
    justifyContent: 'space-between',
    borderBottomWidth: 1,
    borderBottomColor: light2,
    padding: 10,
    marginBottom: 10,
  },
  postsList: {
    flex: 1,
    flexDirection: 'row',
    flexWrap: 'wrap',
    alignItems: 'stretch',
  },
  postItem: {
    width: '49%',
    borderWidth: 1,
    borderRadius: 10,
    borderColor: dark,
    backgroundColor: light,
    padding: 10,
    marginBottom: 10,
    alignContent: 'space-between',
    alignSelf: 'stretch',
    justifyContent: 'space-between',
  },
  positiveScore: {
    fontSize: 16,
    color: success,
    fontWeight: '600',
  },
  negativeScore: {
    fontSize: 16,
    color: danger,
    fontWeight: '600',
  },
});
