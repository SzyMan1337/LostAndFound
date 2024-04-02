import React, { PropsWithChildren } from 'react';
import {
  Pressable,
  SafeAreaView,
  StyleProp,
  StyleSheet,
  Text,
  TextInput,
  TextInputProps,
  TextProps,
  View,
  ViewStyle,
} from 'react-native';
import { ScrollView } from 'react-native-gesture-handler';
import AntDesignIcon from 'react-native-vector-icons/AntDesign';
import { danger, dark, dark2, dark3, light, light2, primary } from './Colors';

export const MainContainer: React.FC<PropsWithChildren> = ({ children }) => {
  return (
    <SafeAreaView style={mainStyles.pageContainer}>{children}</SafeAreaView>
  );
};

export const MainScrollContainer: React.FC<PropsWithChildren> = ({
  children,
}) => {
  return (
    <ScrollView
      contentInset={{ bottom: 80 }}
      style={mainStyles.scrollContainer}>
      <View style={{ marginBottom: 60 }}>{children}</View>
    </ScrollView>
  );
};

export const MainTitle: React.FC<TextProps> = ({ children }) => {
  return <Text style={mainStyles.mainTitle}>{children}</Text>;
};

export const Subtitle: React.FC<TextProps> = ({ children }) => {
  return <Text style={mainStyles.subtitle}>{children}</Text>;
};

export const InputSection: React.FC<
  PropsWithChildren<{
    testID?: string;
    title: string;
  }>
> = ({ children, title, testID }) => {
  return (
    <View style={mainStyles.inputSectionContainer}>
      <Text testID={testID} style={mainStyles.inputSectionTitle}>
        {title}
      </Text>
      {children}
    </View>
  );
};

export const CustomTextInput: React.FC<TextInputProps> = props => {
  return (
    <View style={mainStyles.inputContainer}>
      <TextInput multiline={true} {...props} />
    </View>
  );
};

export const MainButton: React.FC<
  PropsWithChildren<{
    testID?: string;
    label: string;
    onPress: any;
  }>
> = ({ label, onPress, testID }) => {
  return (
    <Pressable
      style={({ pressed }) => [
        mainStyles.mainButton,
        pressed ? { backgroundColor: dark3 } : {},
      ]}
      onPress={onPress}
      testID={testID}>
      <Text style={mainStyles.mainButtonText}>{label}</Text>
    </Pressable>
  );
};

export const SecondaryButton: React.FC<
  PropsWithChildren<{
    testID?: string;
    label: string;
    onPress: any;
  }>
> = ({ label, onPress, testID }) => {
  return (
    <Pressable
      style={({ pressed }) => [
        mainStyles.secondaryButton,
        pressed ? { backgroundColor: dark3 } : {},
      ]}
      onPress={onPress}>
      <Text testID={testID} style={mainStyles.secondaryButtonText}>
        {label}
      </Text>
    </Pressable>
  );
};

export const DeleteButton: React.FC<
  PropsWithChildren<{
    testID?: string;
    label: string;
    onPress: any;
    style?: StyleProp<ViewStyle>;
  }>
> = ({ label, onPress, testID, style }) => {
  return (
    <Pressable style={[mainStyles.deleteButton, style]} onPress={onPress}>
      <Text testID={testID} style={mainStyles.deleteButtonText}>
        {label}
      </Text>
    </Pressable>
  );
};

export const PressableText: React.FC<
  PropsWithChildren<{
    testID?: string;
    text: string;
    onPress: any;
  }>
> = ({ text, onPress, testID }) => {
  return (
    <Pressable onPress={onPress} testID={testID}>
      <Text style={mainStyles.pressableText}>{text}</Text>
    </Pressable>
  );
};

export const ScoreView = (props: { testID?: string; score?: number }) => {
  return (
    <View
      style={{
        flexDirection: 'row',
        justifyContent: 'space-between',
        alignContent: 'center',
      }}>
      <AntDesignIcon name="star" size={25} style={{ color: 'gold' }} />
      <Text testID={props.testID} style={{ fontSize: 18 }}>
        {props.score ? props.score.toPrecision(2) : '0'}
      </Text>
    </View>
  );
};

export const mainStyles = StyleSheet.create({
  pageContainer: {
    flex: 1,
    backgroundColor: light2,
  },
  scrollContainer: {
    padding: 30,
    flex: 1,
  },
  mainTitle: {
    fontSize: 24,
    fontWeight: '600',
    color: dark,
  },
  subtitle: {
    fontSize: 14,
    fontWeight: '400',
    color: dark2,
  },
  inputSectionContainer: {
    paddingTop: 20,
  },
  inputSectionTitle: {
    fontSize: 18,
    fontWeight: '600',
    color: dark,
  },
  inputContainer: {
    borderBottomWidth: 1,
    borderBottomColor: dark2,
  },
  highlight: {
    fontWeight: '700',
  },
  mainButton: {
    alignItems: 'center',
    margin: 10,
    padding: 20,
    backgroundColor: primary,
    borderRadius: 8,
  },
  mainButtonText: {
    fontSize: 20,
    fontWeight: '600',
    color: light,
  },
  secondaryButton: {
    alignItems: 'center',
    padding: 8,
    backgroundColor: primary,
    borderRadius: 5,
  },
  secondaryButtonText: {
    fontSize: 18,
    fontWeight: '600',
    color: light,
  },
  deleteButton: {
    alignSelf: 'flex-end',
    alignItems: 'flex-end',
    padding: 8,
    backgroundColor: danger,
    borderRadius: 5,
  },
  deleteButtonText: {
    fontSize: 18,
    fontWeight: '600',
    color: light,
  },
  pressableText: {
    color: primary,
  },
  pickerStyle: {
    backgroundColor: light,
    borderColor: dark2,
    borderWidth: 1,
    borderRadius: 5,
    marginTop: 5,
  },
});
