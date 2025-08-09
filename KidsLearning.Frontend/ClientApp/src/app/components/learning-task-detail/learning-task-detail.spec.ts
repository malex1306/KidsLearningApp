import { ComponentFixture, TestBed } from '@angular/core/testing';

import { LearningTaskDetail } from './learning-task-detail';

describe('LearningTaskDetail', () => {
  let component: LearningTaskDetail;
  let fixture: ComponentFixture<LearningTaskDetail>;

  beforeEach(async () => {
    await TestBed.configureTestingModule({
      imports: [LearningTaskDetail]
    })
    .compileComponents();

    fixture = TestBed.createComponent(LearningTaskDetail);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
