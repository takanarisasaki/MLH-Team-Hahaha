//
//  http.swift
//  mlh-ios
//
//  Created by Chen He on 2018-12-01.
//  Copyright © 2018 Chen He. All rights reserved.
//

import Foundation


class HTTP{
    
    public static func post(addr:String,data:[String:Any]) -> Void {
        
        
        do {
            let url = URL(string: "https://mlh-team-hahaha.azurewebsites.net/\(addr)")
            let session = URLSession.shared
            
            var request = NSMutableURLRequest(url: url as! URL)
            request.httpMethod = "POST"
            request.addValue("application/json", forHTTPHeaderField: "Content-Type")
            request.httpBody = try JSONSerialization.data(withJSONObject: data, options: .prettyPrinted)
            
            let task = URLSession.shared.dataTask(with: request) { data, response, error in
                guard let data = data, error == nil else {
                    print(error?.localizedDescription ?? "No data")
                    return
                }
                let responseJSON = try? JSONSerialization.jsonObject(with: data, options: [])
                if let responseJSON = responseJSON as? [String: Any] {
                    print(responseJSON)
                }
            }
            
            task.resume()
        } catch  {
            print("failed")
        }
        
    }
}
