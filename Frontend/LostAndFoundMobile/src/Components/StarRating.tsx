import React, { Dispatch, SetStateAction } from 'react';
import {
  StyleSheet,
  View,
  SafeAreaView,
  TouchableWithoutFeedback,
  Animated,
} from 'react-native';
import MaterialIcons from 'react-native-vector-icons/MaterialIcons';

export const StarRating = (props: {
  starRating: number;
  ratingHandler: Dispatch<SetStateAction<number>>;
}) => {
  const starRatingOptions = [1, 2, 3, 4, 5];

  const animatedButtonScale = new Animated.Value(1);

  const handlePressIn = () => {
    Animated.spring(animatedButtonScale, {
      toValue: 1.5,
      useNativeDriver: true,
      speed: 50,
      bounciness: 4,
    }).start();
  };

  const handlePressOut = () => {
    Animated.spring(animatedButtonScale, {
      toValue: 1,
      useNativeDriver: true,
      speed: 50,
      bounciness: 4,
    }).start();
  };

  const animatedScaleStyle = {
    transform: [{ scale: animatedButtonScale }],
  };

  return (
    <SafeAreaView style={{ flex: 1 }}>
      <View style={styles.container}>
        <View style={styles.stars}>
          {starRatingOptions.map(option => (
            <TouchableWithoutFeedback
              onPressIn={() => handlePressIn()}
              onPressOut={() => handlePressOut()}
              onPress={() => props.ratingHandler(option)}
              key={option}>
              <Animated.View style={animatedScaleStyle}>
                <MaterialIcons
                  name={props.starRating >= option ? 'star' : 'star-border'}
                  size={24}
                  style={
                    props.starRating >= option
                      ? styles.starSelected
                      : styles.starUnselected
                  }
                />
              </Animated.View>
            </TouchableWithoutFeedback>
          ))}
        </View>
      </View>
    </SafeAreaView>
  );
};

const styles = StyleSheet.create({
  container: {
    flex: 1,
    alignItems: 'center',
    justifyContent: 'center',
  },
  heading: {
    fontSize: 24,
    fontWeight: 'bold',
    marginBottom: 20,
  },
  stars: {
    display: 'flex',
    flexDirection: 'row',
  },
  starUnselected: {
    color: '#aaa',
  },
  starSelected: {
    color: '#ffb300',
  },
});
