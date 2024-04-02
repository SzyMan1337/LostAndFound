import React, { Dispatch, SetStateAction } from 'react';
import IoniconsIcon from 'react-native-vector-icons/Ionicons';
import DocumentPicker, {
  DocumentPickerResponse,
  types,
} from 'react-native-document-picker';
import {
  Image,
  Dimensions,
  SafeAreaView,
  TouchableOpacity,
  StyleSheet,
  Text,
  View,
} from 'react-native';
import { Colors } from 'react-native/Libraries/NewAppScreen';

export const DocumentSelector = (props: {
  fileResponse: DocumentPickerResponse[];
  setFileResponse: Dispatch<SetStateAction<DocumentPickerResponse[]>>;
  label?: string;
}) => {
  const handleDocumentSelection = React.useCallback(async () => {
    try {
      const response = await DocumentPicker.pick({
        presentationStyle: 'fullScreen',
        type: [types.images],
      });
      props.setFileResponse(response);
    } catch (err) {
      console.warn(err);
    }
  }, []);

  const win = Dimensions.get('window');
  const styles2 = StyleSheet.create({
    image: {
      flex: 1,
      alignSelf: 'stretch',
      width: 0.9 * win.width,
      height: 0.2 * win.height,
    },
  });

  return (
    <SafeAreaView style={styles.container}>
      {props.fileResponse.map((file, index) => (
        <View key={index.toString()}>
          {(file?.size && file.size > 0) ? (
            <View style={styles.photoContainer}>
              <Image
                source={{ uri: file.uri }}
                style={styles2.image}
                resizeMode={'contain'}
              />
            </View>
          ) : (
            <View style={styles.containerForPhoto}>
              <Text style={styles.textInPhotoBox}>Nie dodano zdjęcia</Text>
            </View>
          )}
        </View>
      ))}
      <View style={styles.containerWithButtons}>
        <TouchableOpacity
          style={styles.iconButton}
          onPress={handleDocumentSelection}>
          <IoniconsIcon name="attach-outline" size={25} />
        </TouchableOpacity>
      </View>
    </SafeAreaView>
  );
};

const styles = StyleSheet.create({
  container: {
    margin: 5,
  },
  containerWithButtons: {
    paddingRight: 16,
    flexDirection: 'row',
    justifyContent: 'flex-end',
  },
  iconButton: {
    width: 20,
  },
  textInPhotoBox: {
    fontSize: 16,
    color: 'darkbrown',
    marginTop: 16,
  },
  photoContainer: {
    alignItems: 'center',
    paddingTop: 10,
  },
  containerForPhoto: {
    height: 100,
    alignItems: 'center',
    justifyContent: 'center',
  },
  button: {
    flexDirection: 'row',
    alignItems: 'center',
    padding: 8,
    backgroundColor: 'orange',
    borderRadius: 5,
  },
  buttonText: {
    fontSize: 18,
    fontWeight: '600',
    color: Colors.white,
    marginRight: 5,
  },
  fileNameText: {
    fontSize: 16,
    fontWeight: '400',
    color: 'black',
  },
});
