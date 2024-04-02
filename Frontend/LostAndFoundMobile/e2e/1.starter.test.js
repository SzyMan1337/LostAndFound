describe('Example', () => {
  beforeAll(async () => {
    await device.launchApp();
  });

  beforeEach(async () => {
    await device.reloadReactNative();
  });

  it('should have welcome screen', async () => {
    await expect(element(by.text('E-mail'))).toBeVisible();
    await expect(element(by.text('Hasło'))).toBeVisible();
    await expect(element(by.id('loginButton'))).toBeVisible();
    await expect(element(by.id('registerButton'))).toBeVisible();
  });

  it('should show register form', async () => {
    await element(by.id('registerButton')).tap();
    await expect(element(by.id('emailPlaceholder'))).toBeVisible();
    await expect(element(by.id('usernamePlaceholder'))).toBeVisible();
    await expect(element(by.id('passwordPlaceholder'))).toBeVisible();
    await expect(element(by.id('confirmPasswordPlaceholder'))).toBeVisible();
    await expect(element(by.id('registerButton'))).toBeVisible();
  });
});
