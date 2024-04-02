describe('Example', () => {
  beforeAll(async () => {
    await device.launchApp();
  });

  /*beforeEach(async () => {
    await device.reloadReactNative();
  });*/

  it('should have welcome screen', async () => {
    await expect(element(by.text('E-mail'))).toBeVisible();
    await expect(element(by.text('Hasło'))).toBeVisible();
    await expect(element(by.id('loginButton'))).toBeVisible();
    await expect(element(by.id('registerButton'))).toBeVisible();
  });

  it('should go to register form', async () => {
    await element(by.id('registerButton')).tap();
    await expect(element(by.id('emailPlaceholder'))).toBeVisible();
    await expect(element(by.id('usernamePlaceholder'))).toBeVisible();
    await expect(element(by.id('passwordPlaceholder'))).toBeVisible();
    await expect(element(by.id('confirmPasswordPlaceholder'))).toBeVisible();
    await expect(element(by.id('registerButton'))).toBeVisible();
  });

  it('fill and submit register form', async () => {
    const emailPlaceholder = element(by.id('emailPlaceholder'));
    await emailPlaceholder.typeText('detox1@gmail.com');
    await expect(emailPlaceholder).toHaveText('detox1@gmail.com');

    const usernamePlaceholder = element(by.id('usernamePlaceholder'));
    await usernamePlaceholder.typeText('detox testing1');
    await expect(usernamePlaceholder).toHaveText('detox testing1');

    const passwordPlaceholder = element(by.id('passwordPlaceholder'));
    await passwordPlaceholder.typeText('password');
    await expect(passwordPlaceholder).toHaveText('password');

    const confirmPasswordPlaceholder = element(
      by.id('confirmPasswordPlaceholder'),
    );
    await confirmPasswordPlaceholder.typeText('password');
    await expect(confirmPasswordPlaceholder).toHaveText('password');

    const registerButton = element(by.id('registerButton'));
    await registerButton.tap();
  });

  it('should show login form', async () => {
    await expect(element(by.id('emailPlaceholder'))).toBeVisible();
    await expect(element(by.id('passwordPlaceholder'))).toBeVisible();
    await expect(element(by.id('loginButton'))).toBeVisible();
  });
});
