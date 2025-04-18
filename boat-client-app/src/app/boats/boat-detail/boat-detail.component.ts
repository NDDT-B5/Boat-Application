import { Component } from '@angular/core';
import { BoatDto } from '../../core/models/boat.model';
import { CommonModule } from '@angular/common';
import { ActivatedRoute, Router } from '@angular/router';
import { BoatService } from '../../core/services/boat.service';

@Component({
  selector: 'app-boat-detail',
  imports: [ CommonModule ],
  templateUrl: './boat-detail.component.html',
  styleUrl: './boat-detail.component.scss'
})
export class BoatDetailComponent {
  public boat: BoatDto | null = null;

  constructor(
    private router: Router,
    private boatService: BoatService,
    private route: ActivatedRoute
  ) {}

  ngOnInit() {
    const navigation = this.router.getCurrentNavigation();
    const stateBoat = navigation?.extras?.state?.['boat'] as BoatDto | undefined;

    if (stateBoat) {
      this.boat = stateBoat;
      return;
    }

    const id = this.route.snapshot.paramMap.get('id');
    if (!id) {
      console.warn('No boat data or ID provided.');
      return;
    }

    this.boatService.getById(id).subscribe({
      next: (boat) => this.boat = boat,
      error: () => {
        console.warn('Boat not found or failed to fetch.');
      }
    });
  }
}