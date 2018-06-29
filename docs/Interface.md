### 1. order pay 支付
接口: http://video.wind361.com/api/order/pay?
  
| 参数 | 说明     | 是否必须 
|---- | ------------ |  ---- 
| orderno |  订单号 最大32位, 字母和数字 | 是
| paytype |   alipay 支付宝  weixin 微信   | 是
| orderamt | int 类型, 最小到分  100 = 1元 | 是
| sn | 用户识别号,对应到用户. 可以是`Andriod_ID` 或 App缓存一个ID | 否
| itemname |  购买商品名称 | 否

### 2. order query 查询订单
接口: http://video.wind361.com/api/order/query?

| 参数 | 说明     | 是否必须 
|---- | ------------ |  ---- 
| orderno |  订单号 最大32位, 字母和数字 | 是 

### 3. order history 用户历史订单列表(30天)
接口: http://video.wind361.com/api/order/history?

| 参数 | 说明     | 是否必须 
|---- | ------------ |  ---- 
| sn | 用户识别号,对应到用户. 可以是`Andriod_ID` 或 App缓存一个ID | 是

### 4. new order 创建订单 (暂无)
接口: http://video.wind361.com/api/order/new