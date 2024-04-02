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

  it('login to app', async () => {
    const emailPlaceholder = element(by.id('emailPlaceholder'));
    await emailPlaceholder.typeText('test@gmail.com');
    await expect(emailPlaceholder).toHaveText('test@gmail.com');

    const passwordPlaceholder = element(by.id('passwordPlaceholder'));
    await passwordPlaceholder.typeText('12345678');
    await expect(passwordPlaceholder).toHaveText('12345678');

    const loginButton = element(by.id('loginButton'));
    await loginButton.tap();
  });

  it('should have posts screen', async () => {
    await expect(element(by.id('AddPostButton'))).toBeVisible();
  });
});
