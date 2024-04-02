import EncryptedStorage from 'react-native-encrypted-storage';
import {
  NAME,
  SURNAME,
  USERNAME,
  USER_ID,
  USER_PHOTO_URL,
  USER_RATING,
} from '../../Config';

export async function saveUserId(userId: string) {
  await EncryptedStorage.setItem(USER_ID, userId);
}
export async function getUserId(): Promise<string | null> {
  return await EncryptedStorage.getItem(USER_ID);
}

export async function saveUsername(username: string) {
  await EncryptedStorage.setItem(USERNAME, username);
}
export async function getUsername(): Promise<string | null> {
  return await EncryptedStorage.getItem(USERNAME);
}
export async function removeUsername() {
  await EncryptedStorage.removeItem(USERNAME);
}

export async function saveName(name: string) {
  await EncryptedStorage.setItem(NAME, name);
}
export async function getName(): Promise<string | null> {
  return await EncryptedStorage.getItem(NAME);
}
export async function removeName() {
  await EncryptedStorage.removeItem(NAME);
}

export async function saveSurname(surname: string) {
  await EncryptedStorage.setItem(SURNAME, surname);
}
export async function getSurname(): Promise<string | null> {
  return await EncryptedStorage.getItem(SURNAME);
}
export async function removeSurname() {
  await EncryptedStorage.removeItem(SURNAME);
}

export async function saveUserRating(userRating: string) {
  await EncryptedStorage.setItem(USER_RATING, userRating);
}
export async function getUserRating(): Promise<string | null> {
  return await EncryptedStorage.getItem(USER_RATING);
}

export async function saveUserPhotoUrl(UserPhotoUrl: string) {
  await EncryptedStorage.setItem(USER_PHOTO_URL, UserPhotoUrl);
}
export async function getUserPhotoUrl(): Promise<string | null> {
  return await EncryptedStorage.getItem(USER_PHOTO_URL);
}
export async function removeUserPhotoUrl() {
  await EncryptedStorage.removeItem(USER_PHOTO_URL);
}
