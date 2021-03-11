import React, {useEffect, useState} from 'react';
import useStyles from "./useStyles";
import {CircularProgress, ListItem, ListItemText, TextField} from "@material-ui/core";
import Button from "../button";
import {connect} from "react-redux";
import {RootState} from "../../store/rootReducer";
import {Dispatch} from "redux";
import {CreateAction} from "../../store/actionType";
import {LookupSearchActionType, PaginationActionTypes, State as StateProps} from "./duck";
import {FixedSizeList, ListChildComponentProps} from 'react-window';
import {usePrevious} from "../../helpers";
import {RequestStatuses} from "../../api/requestTypes";
import {useHistory} from 'react-router-dom';


interface DispatchProps {
  lookupRequest: (url: string, params: string) => void;
  incrementPageIndex: () => void;
  clearSearch: () => void;
}
interface Props extends StateProps, DispatchProps {}

const LookupSearch = ({
    incrementPageIndex,
    lookupRequest,
    clearSearch,
    data,
    status,
    error,
  }: Props) => {
  const [search, setSearch] = useState<string>('');
  const [timer, setTimer] = useState<any>(null);
  const styles = useStyles();
  const {
    companies,
    pagination: {
      pageCount,
      pageIndex,
      pageSize,
    },
  } = data;
  const prevSearch = usePrevious(search);
  const prevPageIndex = usePrevious(pageIndex);
  const history = useHistory();

  function renderRow(props: ListChildComponentProps) {
    const { index, style } = props;
    const forText = <div className={styles.result}>
      <span className={styles.bold}>{companies[index].name}</span>,
      <span> {companies[index].country}</span>,
      <span> {companies[index].city}</span>
    </div>

    return (
      <ListItem style={style} key={index} className={styles.listElement}>
        <ListItemText primary={forText} />
      </ListItem>
    );
  }

  useEffect(() => {
    if (timer) {
      clearTimeout(timer);
      setTimer(null);
    }
    const searchChange = search !== prevSearch;
    const pageIndexChange = pageIndex !== prevPageIndex;

    const searchPrefix = `search_prefix=${search}`;
    const pageCountParam = `page_count=${pageCount}`;
    const pageIndexParam = `page_index=${pageIndex}`;
    const pageSizeParam = `page_size=${pageSize}`;
    const params = `${searchPrefix}&${pageCountParam}&${pageIndexParam}&${pageSizeParam}`;
    // TODO: for some reason lookupRequest works 1 of 2 times on search change
    if ((searchChange || pageIndexChange) && search) {
      if (searchChange) clearSearch();
      return setTimer(setTimeout(() => {
        lookupRequest(
          'companiesLookup',
          params
        )
      }, 500));
    }
    if (!search) return clearSearch();
  }, [search, pageIndex, companies]);

  return (<>
    <div className={styles.container}>
      <div className={styles.tabs}>
        <div><span>Companies database</span></div>
      </div>
      <div className={styles.inputContainer}>
        <TextField
          type={'text'}
          size={'small'}
          name={'search'}
          placeholder={'Search...'}
          variant="outlined"
          // error={}
          // helperText={}
          value={search}
          onChange={(e) => setSearch(e.target.value)}
        />
        <Button
          className={`${styles.button}`}
          disabled={!companies.length}
          type={'button'}
          onClick={() => history.push('/results')}
        >
          {
            (status === RequestStatuses.loading && !companies.length) ?
            <CircularProgress size={20} />
            : 'Show all results'
          }
        </Button>
      </div>
      {companies.length > 0 ? <div key={companies.length} className={styles.lookupResults}>
        <FixedSizeList
          className={styles.lookupResultsList}
          itemData={companies}
          height={300}
          width={'100%'}
          itemSize={30}
          itemCount={companies.length}
        >
          {renderRow}
        </FixedSizeList>
          {status !== RequestStatuses.loading ? <button
            className={styles.showMore}
            onClick={incrementPageIndex}
          >SHOW MORE</button>
            :
            <CircularProgress style={{ marginLeft: '40px' }} size={30} />
          }
      </div>
      : null}
    </div>
  </>)
}

export default connect(
  ({ LookupSearch }: RootState) => ({ ...LookupSearch }),

  (dispatch: Dispatch) => ({
    lookupRequest: (url: string, params: string) =>
      dispatch(CreateAction(LookupSearchActionType.LOOKUP_LOAD, {url, params})),
    clearSearch: () => dispatch(CreateAction(LookupSearchActionType.LOOKUP_LOAD_CLEAR)),
    incrementPageIndex: () => dispatch(CreateAction(PaginationActionTypes.INCREMENT)),
  })
)(LookupSearch);
