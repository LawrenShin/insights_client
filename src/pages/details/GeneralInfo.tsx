import React from 'react';
import {Grid} from "@material-ui/core";
import {Rounded} from "../../components/button";
import ExpandMoreIcon from "@material-ui/icons/ExpandMore";
import useStyles from "./useStyles";
import {renderSingleProp} from "./helpers";


const GeneralInfo = ({data}: any) => {
  const styles = useStyles();

  return (<>
    <Grid item sm={12} className={styles.paintContainer}>
      <Grid direction={'row'} container >
        <Grid item className={styles.companyHeader}>
          <Grid direction={'row'} container>
            <span className={styles.titleFont}> {data.companyHeader.name} </span>
            {
              Object.keys(data.companyHeader).map((key) => {
                if (key !== 'name') return renderSingleProp(key, data.companyHeader[key])
              })
            }
          </Grid>
        </Grid>
        <Rounded>EXPORT<ExpandMoreIcon /></Rounded>
      </Grid>
    </Grid>
    <Grid item sm={12} className={`${styles.paintContainer} ${styles.generalContainer}`}>
      <span className={`${styles.titleFont} ${styles.titleSubFontSize}`}>General</span>
      <Grid container justify={"space-between"}>
        <Grid item sm={4}>
          <Grid wrap={'nowrap'} container direction={'row'} className={styles.gap20}>
            <span className={styles.paleFont}>Address:</span>
            <span className={styles.littleFont}>{data.companyGeneral.address}</span>
          </Grid>
        </Grid>
        <Grid item sm={4}>
          <Grid wrap={'nowrap'} container direction={'row'} className={styles.gap20}>
            <span className={styles.paleFont}>ID:</span>
            <div className={`${styles.littleFont} ${styles.flexCol}`}>
              <span>ID: {data.companyGeneral.id}</span>
              {data.companyGeneral.lei && <span>LEI: {data.companyGeneral.lei}</span>}
            </div>
          </Grid>
        </Grid>
        <Grid item sm={3}>
          {!!data.companyGeneral.industries.length &&
          <Grid wrap={'nowrap'} container direction={'row'} className={styles.gap20}>
            <div><span className={styles.paleFont}>Industry:</span></div>
            <div
              className={`${styles.littleFont} ${styles.flexCol}`}
              style={{lineHeight: '2em'}}
            >
              {data.companyGeneral.industries.map((industry: string) =>
                <span key={industry}>{industry}</span>)}
            </div>
          </Grid>}
        </Grid>
      </Grid>
    </Grid>
  </>);
}

export default GeneralInfo;