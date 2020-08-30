/* tslint:disable:no-unused-variable */

import { TestBed, async, inject } from '@angular/core/testing';
import { CostplanService } from './costplan.service';

describe('Service: Costplan', () => {
  beforeEach(() => {
    TestBed.configureTestingModule({
      providers: [CostplanService]
    });
  });

  it('should ...', inject([CostplanService], (service: CostplanService) => {
    expect(service).toBeTruthy();
  }));
});
