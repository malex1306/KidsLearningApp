import { ComponentFixture, TestBed } from '@angular/core/testing';

import { LearningTaskQuiz } from './learning-task-quiz';

describe('LearningTaskQuiz', () => {
  let component: LearningTaskQuiz;
  let fixture: ComponentFixture<LearningTaskQuiz>;

  beforeEach(async () => {
    await TestBed.configureTestingModule({
      imports: [LearningTaskQuiz]
    })
    .compileComponents();

    fixture = TestBed.createComponent(LearningTaskQuiz);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
