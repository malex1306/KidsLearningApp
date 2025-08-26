import {ComponentFixture, TestBed} from '@angular/core/testing';

import {LearningTasks} from './learning-tasks';

describe('LearningTasks', () => {
  let component: LearningTasks;
  let fixture: ComponentFixture<LearningTasks>;

  beforeEach(async () => {
    await TestBed.configureTestingModule({
      imports: [LearningTasks]
    })
      .compileComponents();

    fixture = TestBed.createComponent(LearningTasks);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
