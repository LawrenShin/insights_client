import React, {useState} from 'react';
import LookupSearch from "../../components/lookupSearch";
import CustomSearch from "../../components/customSearch";
import Header from "../../components/Header";
import useStyles from "./useStyles";
import {useStyles as useWelcomeStyled} from '../../components/customSearch/useStyles';
import Welcome from "../../components/forms/register/steps/welcome";


const SearchCompanies = () => {
  const [tab, setTab] = useState<string>('company');
  const styles = useStyles();
  const styles2 = useWelcomeStyled();

  return (
    <div className={styles.root}>
      <Header />
      <div className={styles.searchWrapper}>
        <div className={styles.searchContainer}>
          <LookupSearch tab={tab} setTab={setTab} />
        </div>
        <div className={`${styles.searchSettingsContainer} ${tab === 'company' ? styles.centerContent : ''}`}>
          {/* TODO: some day reconsider this cas custom might also be on companies */}
          {tab !== 'company' && <CustomSearch tab={tab} setTab={setTab} />}
          {tab === 'company' && <Welcome classes={styles2} />}
        </div>
      </div>
    </div>
  )
}

export default SearchCompanies;
