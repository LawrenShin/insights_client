import React from 'react';
import GeneralInfo from "../../../pages/details/GeneralInfo";
import {Grid} from "@material-ui/core";
import Essentials from "../../../pages/details/Essentials";
import RoseNList from "../../../pages/details/RoseNList";
import RatingWrapper, {WrapperModes} from "../../ratings/RatingWrapper";
import Research from "../../charts/Research";
import PieCharts from "../../../pages/details/PieCharts";
import EducationRating from "../../ratings/EducationRating";
import Benchmark from "../../ratings/benchmark";

export const Content = ({data}: any) => {
  const isAdvanced = data?.mode === 'advanced';

  if (!data) return null;

  return (
    <Grid container style={{gap: '5px'}}>
      <GeneralInfo data={data}/>
      {/* essentials */}
      <Grid item sm={12}>
        <Essentials data={data} isAdvanced={isAdvanced}/>
      </Grid>

      {data?.ratingsWindRose && <Grid item sm={12} style={{padding: '0'}}>
        <RoseNList data={data} isAdvanced={isAdvanced}/>
      </Grid>}

      {!isAdvanced && <Grid item sm={12} style={{padding: '0'}}>
        <Grid direction={'row'} container style={{gap: '5px'}} wrap={'nowrap'}>
          <RatingWrapper
            mode={WrapperModes.advanced}
            title={'Research'}
            sm={8}
            style={{display: 'flex', flexDirection: 'column'}}
          >
            <Research/>
          </RatingWrapper>
        </Grid>
      </Grid>}
      <Grid container style={{gap: '5px', padding: '0'}} wrap={'nowrap'}>
        {data?.boardStats && <PieCharts
          height={'150px'}
          title={'Board'}
          data={data.boardStats}
        />}
        {data?.executivesStats && <PieCharts
          height={'150px'}
          title={'Executives'}
          data={data.executivesStats}
        />}
        {data?.educationSubjects?.subjects && <EducationRating
          height={'150px'}
          title={'Education'}
          data={data.educationSubjects.subjects}
        />}
      </Grid>
      <Grid container style={{gap: '5px', padding: '0'}} wrap={'nowrap'}>
        {data?.peerIndustryBenchmark && <RatingWrapper
          title={'Peer Benchmark: Industry'}
          sm={4}
        >
          <Benchmark data={data.peerIndustryBenchmark}/>
        </RatingWrapper>}
        {data?.peerCountryBenchmark && <RatingWrapper
          title={'Peer Benchmark: Geography'}
          sm={4}
        >
          <Benchmark data={data?.peerCountryBenchmark}/>
        </RatingWrapper>}
      </Grid>
    </Grid>
  );
}