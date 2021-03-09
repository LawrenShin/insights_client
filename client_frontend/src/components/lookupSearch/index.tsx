import React, {useEffect, useState} from 'react';
import useStyles from "./useStyles";
import {ListItem, ListItemText, TextField} from "@material-ui/core";
import Button from "../button";
import {connect} from "react-redux";
import {RootState} from "../../store/rootReducer";
import {Dispatch} from "redux";
import {CreateAction} from "../../store/actionType";
import {LookupSearchActionType, State as StateProps} from "./duck";
import {FixedSizeList, ListChildComponentProps} from 'react-window';


interface DispatchProps {
  lookupRequest: (url: string, params: string) => void;
  clearSearch: () => void;
}
interface Props extends StateProps, DispatchProps {}

const Index = ({
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

  function renderRow(props: ListChildComponentProps) {
    const { index, style } = props;
    const forText = <div>
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
    const searchPrefix = `search_prefix=${search}`;
    const pageCountParam = `page_count=${pageCount}`;
    const pageIndexParam = `page_index=${pageIndex}`;
    const pageSizeParam = `page_size=${pageSize}`;
    const params = `${searchPrefix}&${pageCountParam}&${pageIndexParam}&${pageSizeParam}`;

    if (search) {
      return setTimer(setTimeout(() => {
        lookupRequest(
          'companiesLookup',
          params
        )
      }, 500));
    }
    return clearSearch();
  }, [search]);

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
          label={'Search...'}
          variant="outlined"
          // error={}
          // helperText={}
          value={search}
          onChange={(e) => setSearch(e.target.value)}
        />
        <Button
          className={`${styles.button}`}
          disabled={!search}
          type={'button'}
        >
          Show all results
        </Button>
      </div>
      {!!companies.length && <div className={styles.lookupResults}>
        <FixedSizeList
          className={styles.lookupResultsList}
          height={100}
          width={'100%'}
          itemSize={30}
          itemCount={4}
        >
          {renderRow}
        </FixedSizeList>
        <button className={styles.showMore}>SHOW MORE</button>
      </div>}
    </div>
  </>)
}

export default connect(
  ({ LookupSearch }: RootState) => ({ ...LookupSearch }),

  (dispatch: Dispatch) => ({
    lookupRequest: (url: string, params: string) =>
      dispatch(CreateAction(LookupSearchActionType.LOOKUP_LOAD, {url, params})),
    clearSearch: () => dispatch(CreateAction(LookupSearchActionType.LOOKUP_LOAD_CLEAR)),
  })
)(Index);
