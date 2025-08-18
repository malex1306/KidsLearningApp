import { ComponentFixture, TestBed } from '@angular/core/testing';

import { LearningTaskEnglish } from './learning-task-english';

describe('LearningTaskEnglish', () => {
  let component: LearningTaskEnglish;
  let fixture: ComponentFixture<LearningTaskEnglish>;

  beforeEach(async () => {
    await TestBed.configureTestingModule({
      imports: [LearningTaskEnglish]
    })
    .compileComponents();

    fixture = TestBed.createComponent(LearningTaskEnglish);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
