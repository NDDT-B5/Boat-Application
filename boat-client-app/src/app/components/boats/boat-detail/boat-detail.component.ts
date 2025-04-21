import { Component } from '@angular/core';
import { CommonModule } from '@angular/common';
import { ActivatedRoute, Router } from '@angular/router';
import { MatButtonModule } from '@angular/material/button';
import { BoatDto } from '../../../shared/models/boat.model';
import { BoatService } from '../../../core/services/boat.service';
import { BoatsDataSource } from '../../../shared/data/boats-datasource';
import { MatIconModule } from '@angular/material/icon';
import { SnackbarService } from '../../../core/services/snackbar.service';

@Component({
  selector: 'app-boat-detail',
  imports: [ CommonModule, MatButtonModule, MatIconModule ],
  templateUrl: './boat-detail.component.html',
  styleUrl: './boat-detail.component.scss'
})
export class BoatDetailComponent {
  public boat: BoatDto | null = null;

  constructor(
    private router: Router,
    private boatService: BoatService,
    private route: ActivatedRoute,
    private dataSource: BoatsDataSource,
    private snackBarService: SnackbarService
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
      this.snackBarService.showError('No boat data or ID provided.');
      return;
    }

    this.boatService.getById(id).subscribe({
      next: (boat) => this.boat = boat,
      error: () => {
        this.snackBarService.showError('Boat not found or failed to fetch.');
      }
    });
  }

  deleteBoat() {
    if (!this.boat) return;
  
    this.boatService.delete(this.boat.id).subscribe({
      next: () => {
        if (this.boat) {
          this.dataSource.deleteBoat(this.boat.id);
          this.snackBarService.showSuccess(`Boat with id ${this.boat.id} deleted successfully!`);
        }
        this.router.navigate(['/boats']);
      },
      error: () => {
        this.snackBarService.showError('Failed to delete boat!');
      }
    });
  }
  
  goBack() {
    this.router.navigate(['/boats']);
  }
}