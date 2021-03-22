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
import prepareForGrid from "./prepareForGrid";
import {RequestStatuses} from "../../api/requestTypes";


// const styledButton = () => <Rounded>EXPORT<ExpandMoreIcon /></Rounded>

// TODO: types
const Results = (props: any) => {
  const {data, status, error} = props;
  const history = useHistory();
  // TODO: Set it to 3d page go here and see if companies pagi r the same.
  const [pagination, setPagination] = React.useState<PaginationType>(
    data?.pagination || history.location.state as PaginationType
  );
  const styles = useStyles();


  const readyForGrid = data ? prepareForGrid(data): {columns: [], rows: []};

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
          {/* @ts-ignore*/}
          <DataGrid
            onRowDoubleClick={(params) => history.push('/details', params.row)}
            className={styles.dataGrid}
            page={pagination?.pageIndex}
            pageSize={pagination?.pageSize}
            onPageChange={(params) => {
              console.log(pagination)
              setPagination({...pagination, pageIndex: params.page});

            }}
            onCellHover={(params) => console.log(params.value)}
            columnBuffer={4}
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
            loading={!data}
            {...readyForGrid}
          />
        </div>
      </div>
    </div>
  )
}

const connecter = () => connect(
  (state: RootState) => ({
    ...state.Results
  }),
  (dispatch: Dispatch) => ({

  })
)

export default connecter()(Results);
