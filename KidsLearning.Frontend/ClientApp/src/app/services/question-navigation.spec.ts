import { TestBed } from '@angular/core/testing';

import { QuestionNavigation } from './question-navigation';

describe('QuestionNavigation', () => {
  let service: QuestionNavigation;

  beforeEach(() => {
    TestBed.configureTestingModule({});
    service = TestBed.inject(QuestionNavigation);
  });

  it('should be created', () => {
    expect(service).toBeTruthy();
  });
});
