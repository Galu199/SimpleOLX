import { TestBed } from '@angular/core/testing';

import { AdwertsService } from './adwerts.service';

describe('AdwertsService', () => {
  let service: AdwertsService;

  beforeEach(() => {
    TestBed.configureTestingModule({});
    service = TestBed.inject(AdwertsService);
  });

  it('should be created', () => {
    expect(service).toBeTruthy();
  });
});
