import { Component } from '@angular/core';
import { CommonModule } from '@angular/common';
import { ActivatedRoute, Router } from '@angular/router';
import { MatButtonModule } from '@angular/material/button';
import { BoatDto } from '../../../shared/models/boat.model';
import { BoatService } from '../../../core/services/boat.service';
import { BoatsDataSource } from '../../../shared/data/boats-datasource';

@Component({
  selector: 'app-boat-detail',
  imports: [ CommonModule, MatButtonModule ],
  templateUrl: './boat-detail.component.html',
  styleUrl: './boat-detail.component.scss'
})
export class BoatDetailComponent {
  public boat: BoatDto | null = null;

  constructor(
    private router: Router,
    private boatService: BoatService,
    private route: ActivatedRoute,
    public dataSource: BoatsDataSource
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

  deleteBoat() {
    if (!this.boat) return;
  
    this.boatService.delete(this.boat.id).subscribe({
      next: () => {
        if (this.boat)
          this.dataSource.deleteBoat(this.boat.id);
        this.router.navigate(['/boats']);
      },
      error: () => console.warn('Failed to delete boat.')
    });
  }
  
  goBack() {
    this.router.navigate(['/boats']);
  }
}