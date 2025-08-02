import { ComponentFixture, TestBed } from '@angular/core/testing';

import { ParentDashboard } from './parent-dashboard';

describe('ParentDashboard', () => {
  let component: ParentDashboard;
  let fixture: ComponentFixture<ParentDashboard>;

  beforeEach(async () => {
    await TestBed.configureTestingModule({
      imports: [ParentDashboard]
    })
    .compileComponents();

    fixture = TestBed.createComponent(ParentDashboard);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
