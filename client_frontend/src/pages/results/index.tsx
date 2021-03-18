import React from 'react';
import Header from "../../components/Header";
import useStyles from "./useStyles";
import {Typography} from "@material-ui/core";
import {DataGrid, GridToolbarExport} from '@material-ui/data-grid';
import { useDemoData } from '@material-ui/x-grid-data-generator';
import {RootState} from "../../store/rootReducer";
import {connect} from "react-redux";
import {Dispatch} from "redux";
import CustomPagination from "./customPagination";
// import {Rounded} from '../../components/button';
// import ExpandMoreIcon from '@material-ui/icons/ExpandMore';
import {useHistory} from "react-router-dom";
import {Pagination as PaginationType} from "../../components/lookupSearch/duck";



// const styledButton = () => <Rounded>EXPORT<ExpandMoreIcon /></Rounded>


const Results = () => {
  const history = useHistory();
  const [pagination, setPagination] = React.useState<PaginationType>(history.location.state as PaginationType);
  const styles = useStyles();
  const { data } = useDemoData({
    dataSet: 'Commodity',
    rowLength: 100,
    maxColumns: 6,
  });

  return (
    <div className={styles.root}>
      <Header />
      <div className={styles.searchWrapper}>

        <span>Main search</span>
        <Typography variant={'h5'}>
          List of companies
        </Typography>

        <div className={styles.content}>
          {/* TODO: 1) change render of row 2) redirect on row click */}
          <DataGrid
            onRowClick={(params) => history.push('/details', params.row)}
            className={styles.dataGrid}
            page={pagination.pageIndex}
            pageSize={pagination.pageSize}
            onPageChange={(params) =>
              setPagination({...pagination, pageIndex: params.page})}
            pagination
            components={{
              Pagination: (props) =>
                <CustomPagination
                  {...props}
                  pagination={pagination}
                  setPagination={setPagination}
                />,
              // Toolbar: styledButton,
            }}
            hideFooterSelectedRowCount={true}
            {...data}
          />
        </div>
      </div>
    </div>
  )
}

const connecter = () => connect(
  (state: RootState) => ({

  }),
  (dispatch: Dispatch) => ({

  })
)

export default connecter()(Results);
