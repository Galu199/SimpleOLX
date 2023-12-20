import { ComponentFixture, TestBed } from '@angular/core/testing';

import { AdwertsListComponent } from './adwerts-list.component';

describe('AdwertsListComponent', () => {
  let component: AdwertsListComponent;
  let fixture: ComponentFixture<AdwertsListComponent>;

  beforeEach(async () => {
    await TestBed.configureTestingModule({
      declarations: [ AdwertsListComponent ]
    })
    .compileComponents();
  });

  beforeEach(() => {
    fixture = TestBed.createComponent(AdwertsListComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
