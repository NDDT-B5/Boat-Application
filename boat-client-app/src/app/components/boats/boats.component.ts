import { AfterViewInit, ChangeDetectorRef, Component, ViewChild } from '@angular/core';
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
import { SnackbarService } from '../../shared/services/snackbar.service';
import { MatCheckboxModule } from '@angular/material/checkbox';
import { JsonPipe } from '@angular/common';

@Component({
  selector: 'app-boats',
  templateUrl: './boats.component.html',
  styleUrl: './boats.component.scss',
  imports: [
    MatTableModule,
    MatPaginatorModule,
    MatSortModule,
    MatButtonModule,
    MatIconModule,
    MatDialogModule,
    MatCheckboxModule,
    JsonPipe
  ]
})
export class BoatsComponent implements AfterViewInit {
  @ViewChild(MatPaginator) paginator!: MatPaginator;
  @ViewChild(MatSort) sort!: MatSort;
  @ViewChild(MatTable) table!: MatTable<BoatDto>;

  displayedColumns = ['select', 'id', 'name', 'desc', 'actions'];
  selection: BoatDto[] = [];

  constructor(
    public dataSource: BoatsDataSource,
    private dialog: MatDialog,
    private boatService: BoatService,
    private router: Router,
    private snackBarService: SnackbarService,
    private cdr: ChangeDetectorRef) {}


  ngAfterViewInit(): void {
    this.dataSource.sort = this.sort;
    this.dataSource.paginator = this.paginator;
    this.table.dataSource = this.dataSource;

    this.paginator.page.subscribe(() => {
      this.clearSelection();
    });
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
        this.boatService.create(result).subscribe(newBoat => {
          this.dataSource.addBoat(newBoat);
          this.snackBarService.showSuccess(`New Boat "${newBoat.name}" added successfully!`);
        });
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
        this.boatService.update(result.id, result).subscribe({
          next: () => {
            this.dataSource.updateBoat(result);
            this.snackBarService.showSuccess(`Boat with id ${boat.id} updated successfully!`);
          },
          error: () => {
            this.snackBarService.showError(`Failed to update boat with id ${boat.id}!`);
          }
        });
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
          next: () => {
            this.dataSource.deleteBoat(boat.id);
            this.snackBarService.showSuccess(`Boat with id ${boat.id} deleted successfully!`);
          },
          error: () => {
            this.snackBarService.showError(`Failed to delete boat with id ${boat.id}!`);
          }
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


  toggleSelection(row: BoatDto) {
    const index = this.selection.findIndex(b => b.id === row.id);
    if (index >= 0) {
      this.selection.splice(index, 1);
    } else {
      this.selection.push(row);
    }
  }

  isAllSelected(): boolean {
    return this.selection.length === this.dataSource.visibleBoats.length;
  }

  isSomeSelected(): boolean {
    return this.selection.length > 0 && !this.isAllSelected();
  }

  toggleSelectAll(event: any): void {
    const rows = this.dataSource.visibleBoats;
    if (event.checked) {
      rows.forEach(row => {
        if (!this.selection.find(s => s.id === row.id)) {
          this.selection.push(row);
        }
      });
    } else {
      this.selection = this.selection.filter(s => !rows.some(row => row.id === s.id));
    }
  }

  clearSelection(): void {
    this.selection = [];
  }
}