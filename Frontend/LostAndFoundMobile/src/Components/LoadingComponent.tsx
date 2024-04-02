import { StyleSheet, Text, View } from 'react-native';
import { ActivityIndicator } from 'react-native-paper';
import { light2, primary } from './Colors';

export const LoadingView = () => {
  return (
    <View style={styles.mainContainer}>
      <View style={styles.loadingContainer}>
        <ActivityIndicator color={primary} size="large" />
        <Text style={styles.loadingText}>Loading...</Text>
      </View>
    </View>
  );
};

export const LoadingNextPageView = () => {
  return <ActivityIndicator color={primary} size="small" />;
};

const styles = StyleSheet.create({
  mainContainer: {
    justifyContent: 'center',
    flex: 1,
    backgroundColor: light2,
  },
  loadingContainer: {
    alignSelf: 'center',
    paddingHorizontal: 60,
    paddingVertical: 15,
    padding: 20,
    borderRadius: 8,
  },
  loadingText: {
    marginTop: 20,
    fontSize: 16,
    fontWeight: '500',
  },
});
