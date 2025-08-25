import { TestBed } from '@angular/core/testing';

import { QuizLogic } from './quiz-logic';

describe('QuizLogic', () => {
  let service: QuizLogic;

  beforeEach(() => {
    TestBed.configureTestingModule({});
    service = TestBed.inject(QuizLogic);
  });

  it('should be created', () => {
    expect(service).toBeTruthy();
  });
});
