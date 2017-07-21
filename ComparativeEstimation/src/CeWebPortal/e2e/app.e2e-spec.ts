import { CeWebPortalPage } from './app.po';

describe('ce-web-portal App', function() {
  let page: CeWebPortalPage;

  beforeEach(() => {
    page = new CeWebPortalPage();
  });

  it('should display message saying app works', () => {
    page.navigateTo();
    expect(page.getParagraphText()).toEqual('app works!');
  });
});
