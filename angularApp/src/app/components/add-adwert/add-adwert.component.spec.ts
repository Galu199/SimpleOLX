import { ComponentFixture, TestBed } from '@angular/core/testing';

import { AddAdwertComponent } from './add-adwert.component';

describe('AddAdwertComponent', () => {
  let component: AddAdwertComponent;
  let fixture: ComponentFixture<AddAdwertComponent>;

  beforeEach(async () => {
    await TestBed.configureTestingModule({
      declarations: [ AddAdwertComponent ]
    })
    .compileComponents();
  });

  beforeEach(() => {
    fixture = TestBed.createComponent(AddAdwertComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
