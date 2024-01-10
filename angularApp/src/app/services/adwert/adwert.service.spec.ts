import { TestBed } from '@angular/core/testing';

import { AdwertService } from './adwert.service';

describe('AdwertService', () => {
  let service: AdwertService;

  beforeEach(() => {
    TestBed.configureTestingModule({});
    service = TestBed.inject(AdwertService);
  });

  it('should be created', () => {
    expect(service).toBeTruthy();
  });
});
