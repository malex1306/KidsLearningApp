import { ComponentFixture, TestBed } from '@angular/core/testing';

import { LearningLetterTasks } from './learning-letter-tasks';

describe('LearningLetterTasks', () => {
  let component: LearningLetterTasks;
  let fixture: ComponentFixture<LearningLetterTasks>;

  beforeEach(async () => {
    await TestBed.configureTestingModule({
      imports: [LearningLetterTasks]
    })
    .compileComponents();

    fixture = TestBed.createComponent(LearningLetterTasks);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
