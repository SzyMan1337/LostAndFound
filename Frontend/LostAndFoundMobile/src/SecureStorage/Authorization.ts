import { refreshToken } from 'commons';
import EncryptedStorage from 'react-native-encrypted-storage';
import {
  ACCESS_TOKEN,
  REFRESH_TOKEN,
  TOKEN_EXPIRATION_DATE,
} from '../../Config';

export async function clearStorage() {
  await EncryptedStorage.clear();
}

export async function saveRefreshToken(token: string) {
  await EncryptedStorage.setItem(REFRESH_TOKEN, token);
}

export async function getRefreshToken(): Promise<string | null> {
  return await EncryptedStorage.getItem(REFRESH_TOKEN);
}

export async function saveAccessToken(token: string, expirationDate: Date) {
  // save access token
  await EncryptedStorage.setItem(ACCESS_TOKEN, token);

  // save expiration date minus 10 seconds
  // we don't want to use access token in the moment it expires
  expirationDate.setSeconds(expirationDate.getSeconds() - 10);
  await EncryptedStorage.setItem(
    TOKEN_EXPIRATION_DATE,
    expirationDate.toString(),
  );
}

export async function getAccessToken(): Promise<string | null> {
  // check if access token didn't expire
  if (await isTokenExpired()) {
    // get saved refresh token
    const refreshJwtToken = await getRefreshToken();
    if (!refreshJwtToken) return null;

    // refresh access token
    const data = await refreshToken(refreshJwtToken);
    if (data) {
      // save new access token with expiration date
      await saveAccessToken(data.accessToken, data.accessTokenExpirationTime);
    } else {
      await clearStorage();
      return null;
    }
  }

  // get access token
  const accessToken = await EncryptedStorage.getItem(ACCESS_TOKEN);
  return accessToken;
}

async function isTokenExpired() {
  const tokenExpirationDateString = await EncryptedStorage.getItem(
    TOKEN_EXPIRATION_DATE,
  );
  if (!tokenExpirationDateString) return false;

  const expirationDate = new Date(tokenExpirationDateString);
  if (expirationDate < new Date()) {
    return true;
  } else {
    return false;
  }
}
