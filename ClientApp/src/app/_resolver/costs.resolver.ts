import {Injectable} from '@angular/core';
import { Cost } from '../_models/Cost';
import { Resolve, Router } from '@angular/router';
import { CostService } from '../_services/cost.service';
import { catchError } from 'rxjs/operators';
import { of } from 'rxjs';
import { AuthService } from '../_services/auth.service';

@Injectable()

export class CostsResolver implements Resolve<Cost> {
    
    constructor(private costService: CostService,
        private router: Router,
        private authService: AuthService) {}
    
    resolve(): import("rxjs").Observable<Cost> {
        let id = this.authService.decodedToken.nameid;
        return this.costService.getCosts().pipe(
            catchError(error => {
                alert('Hiba tortent az adatok lekerdezese soran!');
                this.router.navigate(['/']);
                return of(null);
            })
        );
    }
}
