import { ComponentFixture, TestBed } from '@angular/core/testing';

import { AdvertViewComponent } from './advert-view.component';

describe('AdvertViewComponent', () => {
  let component: AdvertViewComponent;
  let fixture: ComponentFixture<AdvertViewComponent>;

  beforeEach(async () => {
    await TestBed.configureTestingModule({
      declarations: [ AdvertViewComponent ]
    })
    .compileComponents();
  });

  beforeEach(() => {
    fixture = TestBed.createComponent(AdvertViewComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
