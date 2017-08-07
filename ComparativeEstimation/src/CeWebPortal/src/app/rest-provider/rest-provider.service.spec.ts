/* tslint:disable:no-unused-variable */

import { TestBed, async, inject } from '@angular/core/testing';
import { RestProviderService } from './rest-provider.service';

describe('Service: RestProvider', () => {
  beforeEach(() => {
    TestBed.configureTestingModule({
      providers: [RestProviderService]
    });
  });

  it('should ...', inject([RestProviderService], (service: RestProviderService) => {
    expect(service).toBeTruthy();
  }));
});