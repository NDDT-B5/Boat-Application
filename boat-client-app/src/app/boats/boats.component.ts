import { AfterViewInit, Component, ViewChild } from '@angular/core';
import { MatTableModule, MatTable } from '@angular/material/table';
import { MatPaginatorModule, MatPaginator } from '@angular/material/paginator';
import { MatSortModule, MatSort } from '@angular/material/sort';
import { BoatsDataSource, BoatsItem } from './boats-datasource';
import { MatButtonModule } from '@angular/material/button';
import { MatIconModule } from '@angular/material/icon';
import { MatDialog, MatDialogModule } from '@angular/material/dialog';
import { BoatDeleteDialogComponent } from './boat-delete-dialog/boat-delete-dialog.component';
import { BoatEditCreateDialogComponent } from './boat-edit-create-dialog/boat-edit-create-dialog.component';

@Component({
  selector: 'app-boats',
  templateUrl: './boats.component.html',
  styleUrl: './boats.component.scss',
  imports: [MatTableModule, MatPaginatorModule, MatSortModule, MatButtonModule, MatIconModule, MatDialogModule]
})
export class BoatsComponent implements AfterViewInit {
  @ViewChild(MatPaginator) paginator!: MatPaginator;
  @ViewChild(MatSort) sort!: MatSort;
  @ViewChild(MatTable) table!: MatTable<BoatsItem>;

  constructor(public dataSource: BoatsDataSource, private dialog: MatDialog) {}

  /** Columns displayed in the table. Columns IDs can be added, removed, or reordered. */
  displayedColumns = ['id', 'name', 'desc', 'actions'];

  ngAfterViewInit(): void {
    this.dataSource.sort = this.sort;
    this.dataSource.paginator = this.paginator;
    this.table.dataSource = this.dataSource;
  }

  onAdd() {
    this.dialog.open(BoatEditCreateDialogComponent, {
      data: {
        mode: 'add'
      }
    });
    console.log("Add clicked");
  }
  
  onEdit(boat: BoatsItem) {
    this.dialog.open(BoatEditCreateDialogComponent, {
      data: {
        mode: 'edit',
        boat: boat
      }
    });
    
    console.log("Edit clicked");
  }
  
  onDelete(boat: BoatsItem) {
    this.dialog.open(BoatDeleteDialogComponent, {
      data: {
        boat: boat
      }
    });
    console.log("Delete clicked");
  }

  onDeleteMany() {
    this.dialog.open(BoatDeleteDialogComponent);
    console.log("Delete Many clicked");
  }
}
