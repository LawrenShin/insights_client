import {GridBaseComponentProps} from "@material-ui/data-grid";
import React from "react";
import {FormControl, MenuItem, Select, TextField} from "@material-ui/core";
import Pagination from "@material-ui/lab/Pagination";
import PaginationItem from "@material-ui/lab/PaginationItem";
import useStyles from "./useStyles";


function CustomPagination(props: GridBaseComponentProps) {
  const styles = useStyles();
  const { state, api } = props;
  const [rows, setRows] = React.useState<any>(10);
  const [page, setPage] = React.useState<string>('');

  return (<div className={styles.rootPagination}>
    <label htmlFor={'rowsPerPage'}>Show:&nbsp;</label>
    <FormControl
      size={'small'}
    >
      <Select
        className={styles.rowsPerPageSelect}
        name="rowsPerPage"
        value={rows}
        onChange={(e) => setRows(e.target.value)}
        variant="outlined"
      >
        <MenuItem className={styles.menuItem} value={10}>10</MenuItem>
        <MenuItem value={20}>20</MenuItem>
        <MenuItem value={30}>30</MenuItem>
      </Select>
    </FormControl>

    <Pagination
      // className={styles.paginationField}
      variant="outlined"
      shape="rounded"
      page={state.pagination.page}
      count={state.pagination.pageCount}
      // @ts-expect-error
      renderItem={(props2) => <PaginationItem {...props2} disableRipple />}
      onChange={(event, value) => api.current.setPage(value)}
    />

    <label htmlFor={'moveToPage'}>Move to:&nbsp;</label>
    <TextField
      className={styles.jumpToInput}
      type={'text'}
      size={'small'}
      name={'moveToPage'}
      variant="outlined"
      // error={}
      // helperText={}
      value={page}
      onChange={(e) => setPage(e.target.value)}
    />
  </div>);
}

export default CustomPagination;
