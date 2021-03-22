import React, {useEffect} from 'react';
import Header from "../../components/Header";
import useStyles from "./useStyles";
import {Typography} from "@material-ui/core";
import {GridOverlay, DataGrid, GridToolbarExport} from '@material-ui/data-grid';
import {RootState} from "../../store/rootReducer";
import {connect} from "react-redux";
import {Dispatch} from "redux";
import CustomPagination from "./customPagination";
// import {Rounded} from '../../components/button';
// import ExpandMoreIcon from '@material-ui/icons/ExpandMore';
import {useHistory} from "react-router-dom";
import {Pagination as PaginationType} from "../../components/lookupSearch/duck";
import prepareForGrid from "./prepareForGrid";
import {CreateAction} from "../../store/actionType";
import {ResultsActionType} from "./duck";
import {RequestStatuses} from "../../api/requestTypes";
import LinearProgress from '@material-ui/core/LinearProgress';


function CustomLoadingOverlay() {
  return (
    <GridOverlay>
      <div style={{ position: 'absolute', top: 0, width: '100%' }}>
        <LinearProgress />
      </div>
    </GridOverlay>
  );
}

interface LocStateType {
  pagination: PaginationType;
  search: string;
}

// const styledButton = () => <Rounded>EXPORT<ExpandMoreIcon /></Rounded>

// TODO: types
const Results = (props: any) => {
  const {
    data, status, error,
    resultsRequest,
  } = props;
  const history = useHistory();
  const locState = history.location.state as LocStateType;
  // TODO: Set it to 3d page go here and see if companies pagi r the same.
  const [pagination, setPagination] =
    React.useState<PaginationType>(data?.pagination || locState.pagination);

  const styles = useStyles();

  const readyForGrid = (data && data.companies.length) ? prepareForGrid(data): {columns: [], rows: []};

  useEffect(() => {
    if (pagination) {
      const {pageIndex, pageCount, pageSize} = pagination;
      const params = `search_prefix=${locState.search}&page_index=${pageIndex}&page_count=${pageCount}&page_size=${pageSize}`;
      resultsRequest('companies', params);
    }
  }, [pagination]);

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
          {pagination && <DataGrid
            onRowDoubleClick={(params) => history.push('/details', params.row)}
            className={styles.dataGrid}
            page={pagination.pageIndex}
            pageSize={pagination.pageSize}
            onPageChange={(params) => {
              setPagination({...pagination, pageIndex: params.page});
            }}
            // onCellHover={(params) => console.log(params.value)}
            columnBuffer={4}
            pagination
            paginationMode="server"
            // otherwise doesn't work needs total amount of rows. Also set to 0 if want 'No rows' overlay
            rowCount={readyForGrid.rows.length ? pagination.pageSize * pagination.pageCount : 0}
            components={{
              Pagination: (props) =>
                <CustomPagination
                  {...props}
                  pagination={pagination}
                  setPagination={setPagination}
                />,
              // Toolbar: styledButton,
              LoadingOverlay: CustomLoadingOverlay,
            }}
            hideFooterSelectedRowCount={true}
            loading={!data || status === RequestStatuses.loading}
            {...readyForGrid}
          />}
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
    resultsRequest: (url: string, params: string) =>
      dispatch(CreateAction(ResultsActionType.RESULTS_LOAD, {url, params})),
  })
)

export default connecter()(Results);
