import { AfterViewInit, Component, ViewChild } from '@angular/core';
import { MatTableModule, MatTable } from '@angular/material/table';
import { MatPaginatorModule, MatPaginator } from '@angular/material/paginator';
import { MatSortModule, MatSort } from '@angular/material/sort';
import { MatButtonModule } from '@angular/material/button';
import { MatIconModule } from '@angular/material/icon';
import { MatDialog, MatDialogModule } from '@angular/material/dialog';
import { BoatDeleteDialogComponent } from './boat-delete-dialog/boat-delete-dialog.component';
import { BoatEditCreateDialogComponent } from './boat-edit-create-dialog/boat-edit-create-dialog.component';
import { Router } from '@angular/router';
import { BoatsDataSource } from '../../shared/data/boats-datasource';
import { BoatDto } from '../../shared/models/boat.model';
import { BoatService } from '../../core/services/boat.service';

@Component({
  selector: 'app-boats',
  templateUrl: './boats.component.html',
  styleUrl: './boats.component.scss',
  imports: [MatTableModule, MatPaginatorModule, MatSortModule, MatButtonModule, MatIconModule, MatDialogModule]
})
export class BoatsComponent implements AfterViewInit {
  @ViewChild(MatPaginator) paginator!: MatPaginator;
  @ViewChild(MatSort) sort!: MatSort;
  @ViewChild(MatTable) table!: MatTable<BoatDto>;

  constructor(
    public dataSource: BoatsDataSource,
    private dialog: MatDialog,
    private boatService: BoatService,
    private router: Router) {}

  displayedColumns = ['id', 'name', 'desc', 'actions'];

  ngAfterViewInit(): void {
    this.dataSource.sort = this.sort;
    this.dataSource.paginator = this.paginator;
    this.table.dataSource = this.dataSource;
  }

  onCreate() {
    this.dialog.open(BoatEditCreateDialogComponent, {
      autoFocus: false,
      data: {
        mode: 'add'
      }
    }).afterClosed()
    .subscribe(result => {
      if (result) {
        this.boatService.create(result).subscribe(newBoat => this.dataSource.addBoat(newBoat));
      }
    });
  }
  
  onEdit(boat: BoatDto, event: MouseEvent) {
    event.stopPropagation();
    this.dialog.open(BoatEditCreateDialogComponent, {
      autoFocus: false,
      data: {
        mode: 'edit',
        boat: boat
      }
    }).afterClosed()
    .subscribe(result => {
      if (result) {
        this.boatService.update(result.id, result).subscribe(_ => this.dataSource.updateBoat(result));
      }
    });
  }
  
  onDelete(boat: BoatDto, event: MouseEvent) {
    event.stopPropagation();
    this.dialog.open(BoatDeleteDialogComponent, {
      autoFocus: false,
      data: {
        boat: boat
      }
    }).afterClosed()
    .subscribe((result) => {
      if (result) {
        this.boatService.delete(boat.id).subscribe({
          next: () => this.dataSource.deleteBoat(boat.id),
          error: () => console.warn('Failed to delete boat.')
        });
      }
    });
  }

  onDeleteMany() {
    this.dialog.open(BoatDeleteDialogComponent);
    console.log("Delete Many clicked");
  }

  onRowClick(boat: BoatDto) {
    this.router.navigate(['/boats', boat.id], { state: { boat } });
  }
}
