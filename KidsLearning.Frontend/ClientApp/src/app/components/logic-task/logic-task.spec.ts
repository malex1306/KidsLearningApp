import { ComponentFixture, TestBed } from '@angular/core/testing';

import { LogicTask } from './logic-task';

describe('LogicTask', () => {
  let component: LogicTask;
  let fixture: ComponentFixture<LogicTask>;

  beforeEach(async () => {
    await TestBed.configureTestingModule({
      imports: [LogicTask]
    })
    .compileComponents();

    fixture = TestBed.createComponent(LogicTask);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
