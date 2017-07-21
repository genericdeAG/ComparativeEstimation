/* tslint:disable:no-unused-variable */

import { TestBed, async, inject } from '@angular/core/testing';
import { CeProviderService } from './ce-provider.service';

describe('CeProviderService', () => {
  beforeEach(() => {
    TestBed.configureTestingModule({
      providers: [CeProviderService]
    });
  });

  it('should ...', inject([CeProviderService], (service: CeProviderService) => {
    expect(service).toBeTruthy();
  }));
});
